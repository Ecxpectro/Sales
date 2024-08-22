
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
                _context.Countries.Add(new Country
                {
                    Name = "Brazil",
                    States = new List<State>
                    {
                        new State {
                            Name = "Espirito Santo",
                            Cities = new List<City>
                            {
                                new City
                                {
                                    Name = "Vitória"
                                },
                                new City
                                {
                                    Name = "Vila Velha"
                                },
                                new City
                                {
                                    Name = "Cariacica"
                                }
                            }
                        }
                    }
                });

                _context.Countries.Add(new Country 
                { 
                    Name = "Argentina",
                    States = new List<State>
                    {
                        new State {
                            Name = "Buenos Aires",
                            Cities = new List<City>
                            {
                                new City { Name = "Buenos Aires" },
                                new City {  Name = "La Plata" }
                            }
                        },
                         new State {
                            Name = "Córdoba",
                            Cities = new List<City>
                            {
                                new City { Name = "Córdoba" },
                                new City {  Name = "Villa Carlos Paz" }
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country 
                {
                    Name = "Venezuela",
                    States = new List<State>
                    {
                        new State {
                            Name = "Distrito Capital",
                            Cities = new List<City>
                            {
                                new City { Name = "Caracas" }
                            }
                        },
                         new State {
                            Name = "Miranda",
                            Cities = new List<City>
                            {
                                new City { Name = "Los Teques" },
                                new City {  Name = "Guatire" }
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country 
                {
                    Name = "Perú",
                    States = new List<State>
                    {
                        new State {
                            Name = "Arequipa",
                            Cities = new List<City>
                            {
                                new City { Name = "Arequipa" }
                            }
                        },
                         new State {
                            Name = "Lima",
                            Cities = new List<City>
                            {
                                new City { Name = "Lima" },
                                new City {  Name = "Callao" }
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country 
                { 
                    Name = "Colombia",
                    States = new List<State>
                    {
                        new State {
                            Name = "Bogotá",
                            Cities = new List<City>
                            {
                                new City { Name = "Bogotá" }
                            }
                        },
                         new State {
                            Name = "Antioquia",
                            Cities = new List<City>
                            {
                                new City { Name = "Medellín" },
                                new City {  Name = "Envigado" }
                            }
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
