using AppControle.API.Data;
using AppControle.API.Helpers;
using AppControle.API.Services;
using AppControle.Shared.Entities;
using AppControle.Shared.Enums;
using AppControle.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using SisVendas.Shared.Responses;
using System.Runtime.InteropServices;

namespace SisVendas.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;
        private City city;
        private User user;
        private Client client;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            

            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckCountriesAsync();
            await CheckUserAsync("1010", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "JuanZuluaga.jpeg", UserType.Admin);
            await CheckUserAsync("2020", "Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "LedysBedoya.jpeg", UserType.User);
            await CheckUserAsync("3030", "Brad", "Pitt", "brad@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Brad.jpg", UserType.User);
            await CheckUserAsync("4040", "Angelina", "Jolie", "angelina@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Angelina.jpg", UserType.User);
            await CheckUserAsync("5050", "Bob", "Marley", "bob@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "bob.jpg", UserType.User);
            city = await _context.Cities!.FirstOrDefaultAsync();
            user = await _context.Users!.FirstOrDefaultAsync(c => c.Cpf_Cnpj == "1010")!;
            await CheckProductsAsync();
            await CheckClientsAsync();
            client = await _context.Clients!.FirstOrDefaultAsync()!;
            await CheckMonthlyFeeAsync();
        }

        private async Task CheckMonthlyFeeAsync()
        {
            if(!_context.MonthlyFee.Any())
            {
                _context.MonthlyFee.Add(new MonthlyFee
                {
                    Client = client,
                    ClientId = client.Id,
                    PaymentMethod = PaymentMethod.Dinheiro,
                    Reference = DateTime.Now.AddDays(10),
                    User = user,
                    UserId = user.Id,
                    Value = 500,
                    DueDate = DateTime.Now.AddDays(10),

                });
                _context.MonthlyFee.Add(new MonthlyFee
                {
                    Client = client,
                    ClientId = client.Id,
                    PaymentMethod = PaymentMethod.Dinheiro,
                    Reference = DateTime.Now.AddDays(15),
                    Value = 500,
                    User = user,
                    UserId = user.Id,
                    DueDate = DateTime.Now.AddDays(15),

                }); _context.MonthlyFee.Add(new MonthlyFee
                {
                    Client = client,
                    ClientId = client.Id,
                    Payday = DateTime.Now.AddDays(1),
                    PaymentMethod = PaymentMethod.Dinheiro,
                    Reference = DateTime.Now.AddDays(7),
                    Value = 500,
                    DueDate = DateTime.Now.AddDays(7),
                    User = user,
                    UserId = user.Id,

                }); _context.MonthlyFee.Add(new MonthlyFee
                {
                    Client = client,
                    ClientId = client.Id,
                    PaymentMethod = PaymentMethod.Dinheiro,
                    Reference = DateTime.Now.AddDays(100),
                    Value = 500,
                    DueDate = DateTime.Now.AddDays(100),
                    User = user,
                    UserId = user.Id,

                }); _context.MonthlyFee.Add(new MonthlyFee
                {
                    Client = client,
                    ClientId = client.Id,
                    PaymentMethod = PaymentMethod.Dinheiro,
                    Reference = DateTime.Now.AddDays(34),
                    Value = 500,
                    DueDate = DateTime.Now.AddDays(34),
                    User = user,
                    UserId = user.Id,

                }); _context.MonthlyFee.Add(new MonthlyFee
                {
                    Client = client,
                    ClientId = client.Id,
                    PaymentMethod = PaymentMethod.Dinheiro,
                    Reference = DateTime.Now.AddDays(200),
                    Value = 500,
                    DueDate = DateTime.Now.AddDays(200),
                    User = user,
                    UserId = user.Id,

                });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "adidas_barracuda.png" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "Adidas_superstar.png" });
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "airpos.png", "airpos2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "audifonos_bose.png" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Deportes" }, new List<string>() { "bicicleta_ribble.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Ropa" }, new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "casco_bicicleta.png", "casco.png" });
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "iphone13.png", "iphone13b.png", "iphone13c.png", "iphone13d.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "mancuernas.png" });
                await AddProductAsync("Mascarilla Cara", 26000M, 100F, new List<string>() { "Belleza" }, new List<string>() { "mascarilla_cara.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance530.png" });
                await AddProductAsync("New Balance 565", 179000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance565.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_zoom.png" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Suplemento Boots Original", 15600M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "Boost_Original.png" });
                await AddProductAsync("Whey Protein", 252000M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "whey_protein.png" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "cama_mascota.png" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "mouse_gamer.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product prodcut = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                lProductCategories = new List<ProductCategory>(),
                lProductImages = new List<ProductImage>(),
                User = user,
                UserId = user.Id,
                IsService = true,
            };

            foreach (var categoryName in categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                if (category != null)
                {
                    prodcut.lProductCategories.Add(new ProductCategory { Category = category });
                }
            }

            foreach (string? image in images)
            {
                string filePath;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = $"{Environment.CurrentDirectory}\\Images\\products\\{image}";
                }
                else
                {
                    filePath = $"{Environment.CurrentDirectory}/Images/products/{image}";
                }

                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "products");
                prodcut.lProductImages.Add(new ProductImage { Image = imagePath });
            }

            _context.Products.Add(prodcut);
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos E Remedios em geral Testte grande" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erótica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardín" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckClientsAsync()
        {
            ClientService cli = new();
            cli.Product = await _context.Products.FirstOrDefaultAsync();

            if (!_context.Clients.Any())
            {
                _context.Clients.Add(new Client
                {
                    Name = "Client 1",
                    Cpf_Cnpj = "38713376845",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id,
                    lClientService = new List<ClientService>()
                    {
                        cli,
                    },
                });
                _context.Clients.Add(new Client
                {
                    Name = "Client 2",
                    Cpf_Cnpj = "06960118806",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118805",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });

                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118803",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118804",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118802",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118801",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118800",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118839",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118838",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118837",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118835",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960412835",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118834",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118833",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id
                });
                _context.Clients.Add(new Client
                {
                    Name = "Teste cliente nome",
                    Cpf_Cnpj = "06960118832",


                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    CityId = city.Id,

                    User = user,
                    UserId = user.Id,
                    
                });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities!.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities!.FirstOrDefaultAsync();
                }

                //string filePath;
                //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                //{
                //    filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                //}
                //else
                //{
                //    filePath = $"{Environment.CurrentDirectory}/Images/users/{image}";
                //}

                //var fileBytes = File.ReadAllBytes(filePath);
                //var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    Name = firstName + " " + lastName,
                    Cpf_Cnpj = document,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    City = city,
                    UserType = userType,
                    
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }


        private async Task CheckCountriesAsync()
        {

            if (!_context.Countries.Any())
            {
                Response responseCountries = await _apiService.GetListAsync<ResponseApiCities>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    List<ResponseApiCities> countries = (List<ResponseApiCities>)responseCountries.Result!;
                    foreach (ResponseApiCities countryResponse in countries)
                    {
                        if(countryResponse.Name == "Brazil" || countryResponse.Name == "Italy")
                        {
                            Country? country = await _context.Countries!.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                            if (country == null)
                            {
                                country = new() { Name = countryResponse.Name!, lStates = new List<State>() };
                                Response responseStates = await _apiService.GetListAsync<ResponseApiCities>("/v1", $"/countries/{countryResponse.Iso2}/states");
                                if (responseStates.IsSuccess)
                                {
                                    List<ResponseApiCities> states = (List<ResponseApiCities>)responseStates.Result!;
                                    foreach (ResponseApiCities stateResponse in states!)
                                    {
                                        
                                        State state = country.lStates!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                        if (state == null)
                                        {
                                            state = new() { Name = stateResponse.Name!, lCities = new List<City>() };
                                            Response responseCities = await _apiService.GetListAsync<ResponseApiCities>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                            if (responseCities.IsSuccess)
                                            {
                                                List<ResponseApiCities> cities = (List<ResponseApiCities>)responseCities.Result!;
                                                foreach (ResponseApiCities cityResponse in cities)
                                                {
                                                    if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                    {
                                                        continue;
                                                    }
                                                    City city = state.lCities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                    if (city == null)
                                                    {
                                                        state.lCities.Add(new City() { Name = cityResponse.Name! });
                                                    }

                                                    country.lStates.Add(state);
                                                    //_context.Countries.Add(country);
                                                    //await _context.SaveChangesAsync();
                                                    //return;
                                                }

                                            }
                                            
                                            if (state.lCities.Count > 0)
                                            {
                                                country.lStates.Add(state);
                                            }
                                        }
                                    }
                                }
                                if (country.lStates.Count > 0)
                                {
                                    _context.Countries.Add(country);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
