namespace UniTests.Core.Features.Commands.CreateShipment;

public class CreateShipmentCommandHandlerTest
{
    private readonly Mock<IWritePackageRepository> _writePackageRepository;
    private readonly Mock<IPackageWithPackagesToBagsSpecification> _packageWithPackagesToBagsSpecificationMock;
    private readonly Mock<IBagWithPackagesToBagsSpecification> _bagWithPackagesToBagsSpecificationMock;
    private readonly Mock<IWriteBagRepository> _writeBagRepositoryMock;
    private readonly Mock<IWriteWrongDeliveryLogRepository> _writeWrongDeliveryLogRepositoryMock;
    private readonly Mock<IAppLogger<CreateShipmentCommandHandler>> _appLoggerMock;
    private readonly CreateShipmentCommandHandler _createShipmentCommandHandler;

    public CreateShipmentCommandHandlerTest()
    {
        _writePackageRepository = new Mock<IWritePackageRepository>();
        _packageWithPackagesToBagsSpecificationMock = new Mock<IPackageWithPackagesToBagsSpecification>();
        _bagWithPackagesToBagsSpecificationMock = new Mock<IBagWithPackagesToBagsSpecification>();
        _writeBagRepositoryMock = new Mock<IWriteBagRepository>();
        _writeWrongDeliveryLogRepositoryMock = new Mock<IWriteWrongDeliveryLogRepository>();
        _appLoggerMock = new Mock<IAppLogger<CreateShipmentCommandHandler>>();

        _createShipmentCommandHandler = new CreateShipmentCommandHandler(_writePackageRepository.Object, 
            _writeBagRepositoryMock.Object, 
            _bagWithPackagesToBagsSpecificationMock.Object, 
            _packageWithPackagesToBagsSpecificationMock.Object, 
            _writeWrongDeliveryLogRepositoryMock.Object, 
            _appLoggerMock.Object);
    }

    [Fact]
    private async Task CorrectlyPackageUnloadWhenTransferedToBranchPoint()
    {
        // Given

        var deliveries = new List<Delivery>()
                    {
                        new Delivery()
                        {
                            Barcode = "P1",
                        }
                    };

        var packagesRequest = new CreateShipmentCommandRequest(){
            Plate = "PLATE",
            Route = new List<Route>()
            {
                new Route()
                {
                    DeliveryPoint = FleetManagement.Domain.Enums.DeliveryPoints.Branch,
                    Deliveries = deliveries
                }
            }
        };
        var package = new Package(deliveries[0].Barcode, "1", 1);
        var bag = new Bag("C32", "1");

        var packagesToBags = new PackagesToBags()
        {
            Bag = bag,
            BagId = "C32",
            Package = package,
            PackageId = "P1"
        };

        var packages = new List<Package>() {package.AddPackageToBag(packagesToBags) };

        _packageWithPackagesToBagsSpecificationMock.Setup(repo => repo.GetPackage(deliveries[0].Barcode)).Returns(packages.AsQueryable);

        // When
        var response = await _createShipmentCommandHandler.Handle(packagesRequest, CancellationToken.None);

        // Then 

        var route = response.Route.ToList()[0];

        Assert.NotNull(route);
        Assert.Equal(deliveries[0].Barcode, route.Deliveries.ToList()[0].Barcode);
        Assert.Equal((int)PackageStatus.Unloaded, route.Deliveries.ToList()[0].State);
    }

    [Fact]
    private async Task CorrectlyAllShipmentsUnloadWhenTransferedToDistributionCenterPoint()
    {
        // Given
        string bagId = "12";

        var deliveries = new List<Delivery>()
                    {
                        new Delivery()
                        {
                            Barcode = "P2",
                        }
                    };

        var package = new Package(deliveries[0].Barcode, "2", 5);
        var bag = new Bag("b5","2");

        var packagesRequest = new CreateShipmentCommandRequest(){
            Plate = "PLATE 2",
            Route = new List<Route>()
            {
                new Route()
                {
                    DeliveryPoint = FleetManagement.Domain.Enums.DeliveryPoints.DistributionCenter,
                    Deliveries = deliveries
                }
            }
        };

        var packages = new List<Package>() 
        { 
            package
            .AddPackageToBag(new PackagesToBags()
            {
                PackageId = deliveries[0].Barcode,
                BagId = bagId,
                Package = package,
                Bag = bag

            }) 
        };

        _packageWithPackagesToBagsSpecificationMock
            .Setup(repo => repo.GetPackage(deliveries[0].Barcode))
            .Returns(packages.AsQueryable);

        // When
        var response = await _createShipmentCommandHandler.Handle(packagesRequest, CancellationToken.None);

        // Then 

        var route = response.Route.ToList()[0];

        Assert.NotNull(route);
        Assert.Equal(deliveries[0].Barcode, route.Deliveries.ToList()[0].Barcode);
        Assert.Equal((int)PackageStatus.Unloaded, route.Deliveries.ToList()[0].State);
    }

    [Fact]
    private async Task CorrectlyBagAndPackagesInBagUnloadWhenTransferedToTransferCenterPoint()
    {
        // Given

        var deliveries = new List<Delivery>()
                    {
                        new Delivery()
                        {
                            Barcode = "C44",
                        }
                    };

        var package = new Package("P34", "3", 25);
        var bag = new Bag("C44", "3");
        var packagesToBags = new PackagesToBags()
            {
                Bag = bag,
                BagId = "C44",
                Package = package,
                PackageId = "P34"
            };


        // Bag Db for _bagWithPackagesToBagsSpecificationMock
        var bags = new List<Bag>() { bag.AddPackagesToBags(packagesToBags) };


        // shipment
        var packagesRequest = new CreateShipmentCommandRequest()
        {
            Plate = "PLATE 3",
            Route = new List<Route>()
            {
                new Route()
                {
                    DeliveryPoint = FleetManagement.Domain.Enums.DeliveryPoints.TransferCenter,
                    Deliveries = deliveries
                }
            }
        };

        _bagWithPackagesToBagsSpecificationMock
            .Setup(repo => repo.Bags(deliveries[0].Barcode))
            .Returns(bags.AsQueryable);
        //When
        var response = await _createShipmentCommandHandler.Handle(packagesRequest, CancellationToken.None);


        //Then
        var route = response.Route.ToList()[0];
        Assert.NotNull(route);
        Assert.Equal(deliveries[0].Barcode, route.Deliveries.ToList()[0].Barcode);
        Assert.Equal((int)PackageStatus.Unloaded, route.Deliveries.ToList()[0].State);
    }
}
