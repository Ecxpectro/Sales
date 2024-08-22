
using Sales.Shared.Entities;

namespace Sales.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country {Name = "Brazil"});
                _context.Countries.Add(new Country {Name = "Argentina"});
                _context.Countries.Add(new Country {Name = "Venezuela"});
                _context.Countries.Add(new Country {Name = "Perú"});
                _context.Countries.Add(new Country {Name = "Colombia"});
                await _context.SaveChangesAsync();
            }
        }
    }
}
