using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetManagement.Application.Interfaces.Repositories.Package;

namespace FleetManagement.Application.Features.Commands.CreatePackage
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommandRequest, CreatePackageCommandResponse>
    {
        readonly IWritePackageRepository _writePackageRepository;

        public CreatePackageCommandHandler(IWritePackageRepository writePackageRepository)
        {
            _writePackageRepository = writePackageRepository;
        }

        public async Task<CreatePackageCommandResponse> Handle(CreatePackageCommandRequest request, CancellationToken cancellationToken)
        {
            await _writePackageRepository.AddAsync(new(request.Barcode, request.DeliveryPointId, request.VolumetricWeight));
            await _writePackageRepository.SaveAsync();
            return new();
        }
    }
}
