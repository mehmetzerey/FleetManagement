namespace FleetManagement.Application.Features.Commands.CreateShipment;

public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommandRequest, CreateShipmentCommandResponse>
{
    private readonly IWritePackageRepository _writePackage;
    private readonly IPackageWithPackagesToBagsSpecification _readPackage;
    private readonly IBagWithPackagesToBagsSpecification bagWithPackagesToBagsSpecification;
    private readonly IWriteBagRepository _writeBagRepository;
    private readonly IWriteWrongDeliveryLogRepository _writeWrongDeliveryLogRepository;
    private readonly IAppLogger<CreateShipmentCommandHandler> _appLogger;

    public CreateShipmentCommandHandler(IWritePackageRepository writePackage,
        IWriteBagRepository writeBagRepository,
        IBagWithPackagesToBagsSpecification bagWithPackagesToBagsSpecification,
        IPackageWithPackagesToBagsSpecification readPackage,
        IWriteWrongDeliveryLogRepository writeWrongDeliveryLogRepository,
        Interfaces.IAppLogger<CreateShipmentCommandHandler> appLogger)
    {
        _writePackage = writePackage;
        _writeBagRepository = writeBagRepository;
        this.bagWithPackagesToBagsSpecification = bagWithPackagesToBagsSpecification;
        _readPackage = readPackage;
        _writeWrongDeliveryLogRepository = writeWrongDeliveryLogRepository;
        _appLogger = appLogger;
    }

    public async Task<CreateShipmentCommandResponse> Handle(CreateShipmentCommandRequest request, CancellationToken cancellationToken)
    {
        // I update the status to “loaded” for all packages and bags that are specified in the list and loaded into the assigned bags
        var routes = await StatusOfAllPackagesAndBagsUpdatedLoaded(request);

        var routeList = new List<ResponseObject.Route>();

        await UnloadShipmentsToDeliveryPoints(routes, routeList);

        var shipmentResponse = new CreateShipmentCommandResponse
        {
            Plate = request.Plate,
            Route = routeList
        };

        return shipmentResponse;
    }

    private async Task<List<Route>> StatusOfAllPackagesAndBagsUpdatedLoaded(CreateShipmentCommandRequest request)
    {
        var routes = request.Route.ToList();
        foreach (var route in routes)
        {

            foreach (var delivery in route.Packages)
            {
                var package = _readPackage.GetPackage(delivery.Barcode).FirstOrDefault() ?? new Entity.Package();
                package.UpdatePackageStatus(Domain.Enums.PackageStatus.Loaded);
                _writePackage.Update(package);
            }

            foreach (var deliveryBag in route.Bags)
            {
                var bag = bagWithPackagesToBagsSpecification.Bags(deliveryBag.Barcode).FirstOrDefault() ?? new Entity.Bag();
                bag.UpdateBagStatus(Domain.Enums.BagStatus.Loaded);
                _writeBagRepository.Update(bag);
                if (bag.TotalPackages > 0)
                {
                    var packageInTheBag = bag.PackagesToBags;
                    foreach (var pac in packageInTheBag)
                    {
                        pac.Package.UpdatePackageStatus(Domain.Enums.PackageStatus.Loaded);
                        _writePackage.Update(pac.Package);
                    }

                }
            }
        }
        await _writePackage.SaveAsync();
        return routes;
    }

    private async Task UnloadShipmentsToDeliveryPoints(List<Route> routes, List<ResponseObject.Route> routeList)
    {
        foreach (var route1 in routes)
        {
            var deliveryPoint = route1.DeliveryPoint;
            var deliveryList = new List<ResponseObject.Delivery>();
            // First I deliver the packages and in the bag to the delivery point then the bag and the packages in the bag


            await ShipmentUnloadingForPackagesAndPackagesInBag(route1, deliveryPoint, deliveryList);


            await ShipmentUnloadingForBagsAndPackagesInBag(route1, deliveryPoint, deliveryList);


            routeList.Add(new ResponseObject.Route { DeliveryPoint = route1.DeliveryPoint, Deliveries = deliveryList });
        }
        await _writeBagRepository.SaveAsync();
    }

    private async Task ShipmentUnloadingForPackagesAndPackagesInBag(Route route1, DeliveryPoints deliveryPoint, List<ResponseObject.Delivery> deliveryList)
    {
        foreach (var delivery in route1.Packages)
        {
            var dbPackage = _readPackage.GetPackage(delivery.Barcode).FirstOrDefault() ?? new Entity.Package();

            if (dbPackage.DeliveryPointId == Convert.ToInt32(DeliveryPoints.Branch).ToString() && 
                deliveryPoint == DeliveryPoints.Branch)
            {
                dbPackage.UpdatePackageStatus(Domain.Enums.PackageStatus.Unloaded);
                _writePackage.Update(dbPackage);
                deliveryList.Add(new ResponseObject.Delivery { Barcode = delivery.Barcode, State = (int)dbPackage.PackageStatus });
                continue;
            }


            if (
                 deliveryPoint == DeliveryPoints.DistributionCenter &&
                 dbPackage.DeliveryPointId == Convert.ToInt32(DeliveryPoints.DistributionCenter).ToString())
            {
                dbPackage.UpdatePackageStatus(Domain.Enums.PackageStatus.Unloaded);
                if (dbPackage.PackagesToBags != null)
                {
                    var bag = dbPackage.PackagesToBags.Bag;
                    bag.UpdateBagStatus(BagStatus.Unloaded);

                    _writeBagRepository.Update(bag);
                }
                _writePackage.Update(dbPackage);
                deliveryList.Add(new ResponseObject.Delivery { Barcode = delivery.Barcode, State = (int)dbPackage.PackageStatus });
                continue;
            }
            // 
            if (deliveryPoint == DeliveryPoints.TransferCenter && 
                dbPackage.DeliveryPointId == Convert.ToInt32(DeliveryPoints.TransferCenter).ToString())
            {
                UnloadBagAndPackageInBagWhenPackageTransferToTransferCenter(deliveryList, delivery, dbPackage);
                continue;
            }

            await _writeWrongDeliveryLogRepository.AddAsync(new Domain.Entities.WrongDeliveryLog(dbPackage.DeliveryPointId, dbPackage.Id));
            _appLogger.LogWarning(dbPackage.Id + " " + "attempt to deliver to the wrong point.");
            // The packages are going to the wrong address

        }
    }

    private async Task ShipmentUnloadingForBagsAndPackagesInBag(Route route1, DeliveryPoints deliveryPoint, List<ResponseObject.Delivery> deliveryList)
    {
        foreach (var deliveryBag in route1.Bags)
        {
            var bag = bagWithPackagesToBagsSpecification.Bags(deliveryBag.Barcode).FirstOrDefault();
            if (deliveryPoint == DeliveryPoints.DistributionCenter || deliveryPoint == DeliveryPoints.TransferCenter)
            {
                bag.UpdateBagStatus(Domain.Enums.BagStatus.Unloaded);
                _writeBagRepository.Update(bag);

                var packageInTheBag = bag.PackagesToBags.Select(x => x.Package).ToList();
                foreach (var package in packageInTheBag)
                {
                    package.UpdatePackageStatus(Domain.Enums.PackageStatus.Unloaded);
                    _writePackage.Update(package);
                }
            }
            else
            {
                _appLogger.LogWarning(bag.Id + " " + "attempt to deliver to the wrong point.");
                await _writeWrongDeliveryLogRepository.AddAsync(new Domain.Entities.WrongDeliveryLog(bag.DeliveryPointId, bag.Id));
                // çantalar yanlış adrese gidiyor
            }
            deliveryList.Add(new ResponseObject.Delivery { Barcode = deliveryBag.Barcode, State = (int)bag.BagStatus });
        }
    }

    

    private void UnloadBagAndPackageInBagWhenPackageTransferToTransferCenter(List<ResponseObject.Delivery> deliveryList, Delivery delivery, Package package)
    {
        if (package.PackagesToBags != null)
        {
            package.UpdatePackageStatus(Domain.Enums.PackageStatus.Unloaded);
            var bag = package.PackagesToBags.Bag;
            bag.UpdateBagStatus(BagStatus.Unloaded);

            _writeBagRepository.Update(bag);
        }
        _writePackage.Update(package);
        deliveryList.Add(new ResponseObject.Delivery { Barcode = delivery.Barcode, State = (int)package.PackageStatus });
    }
}
