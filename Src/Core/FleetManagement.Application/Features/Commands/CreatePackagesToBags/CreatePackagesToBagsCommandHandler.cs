using FleetManagement.Application.Interfaces.Repositories.Bag;
using FleetManagement.Application.Interfaces.Repositories.Package;
using FleetManagement.Domain.Entities;

namespace FleetManagement.Application.Features.Commands.CreatePackageToBag;

public class CreatePackagesToBagsCommandHandler : IRequestHandler<CreatePackagesToBagsCommandRequest, CreatePackagesToBagsCommandResponse>
{
    private readonly IWritePackagesToBagsRepository _writePackagesToBagsRepository;

    private readonly IReadPackageRepository _readPackageRepository;
    private readonly IWritePackageRepository _writePackageRepository;

    private readonly IReadBagRepository _readBagRepository;
    private readonly IWriteBagRepository _writeBagRepository;



    public CreatePackagesToBagsCommandHandler(
        IWritePackagesToBagsRepository writePackagesToBagsRepository,
        IReadPackageRepository readPackageRepository,
        IReadBagRepository readBagRepository,
        IWritePackageRepository writePackageRepository,
        IWriteBagRepository writeBagRepository)
    {
        _writePackagesToBagsRepository = writePackagesToBagsRepository;
        _readPackageRepository = readPackageRepository;
        _readBagRepository = readBagRepository;
        _writePackageRepository = writePackageRepository;
        _writeBagRepository = writeBagRepository;
    }

    public async Task<CreatePackagesToBagsCommandResponse> Handle(CreatePackagesToBagsCommandRequest request, CancellationToken cancellationToken)
    {
        var getPackage = await _readPackageRepository.GetByIdAsync(request.PackageId);
        var getBag = await _readBagRepository.GetByIdAsync(request.BagId);


        PackageOrBagnNullAndDeliveryPointValidation(getPackage, getBag);

        getPackage.UpdatePackageStatus(Domain.Enums.PackageStatus.LoadedIntoBag);
        _writePackageRepository.Update(getPackage);

        getBag.UpdateBagStatus(Domain.Enums.BagStatus.Created);
        _writeBagRepository.Update(getBag);
        var result = await _writePackagesToBagsRepository.AddAsync(new() { PackageId = request.PackageId, BagId = request.BagId });

        await _writePackagesToBagsRepository.SaveAsync();
        return new(result);
    }

    public  void PackageOrBagnNullAndDeliveryPointValidation(Package getPackage, Bag getBag)
    {
        if (getPackage == null || getBag == null)
            throw new Exception("PacketId or BagId not found in db.");

        if (getPackage.DeliveryPointId != getBag.DeliveryPointId)
            throw new Exception("The delivery point of the package and the bag is not the same.");
    }
}
