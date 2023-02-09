namespace IntegrationTests.Repositories.PackageRepositoryTests;

public class UpdatePackageStatus
{
    private readonly AppDbContext _context;
    private readonly WriteRepository<Package> _packageRepository;

    public UpdatePackageStatus()
    {
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestApp")
            .Options;

        _context = new AppDbContext(dbOptions);
        _packageRepository = new WriteRepository<Package>(_context);
    }

    [Fact]
    public async Task UpdatePackageState()
    {
        //default package state = created
        var package = new Package("p66", "1", 45);
        await _packageRepository.AddAsync(package);
        await _packageRepository.SaveAsync();

        package.UpdatePackageStatus(FleetManagement.Domain.Enums.PackageStatus.Loaded);

        _packageRepository.Update(package);
        await _packageRepository.SaveAsync();

        Assert.Equal(FleetManagement.Domain.Enums.PackageStatus.Loaded, package.PackageStatus);
    }
}
