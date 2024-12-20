﻿//using AppControle.API.Helpers;
//using AppControle.API.Services;
//using Shared.Enums;
//using Shared.Responses;
//using SisVendas.Shared.Responses;
//using System.Runtime.InteropServices;

using AppControle.API.Context;
using AppControle.API.Repositories;
using AppControle.API.Services;
using Shared.Entities;
using Shared.Enums;
using Shared.Response;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;

namespace SisVendas.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserRepository _userRepository;
        //        private readonly IFileStorage _fileStorage;
        private City city;
        private User user;
        private Client client;

        public SeedDb(DataContext context, IUserRepository userRepository, IApiService apiService) // IUserHelper userHelper, IFileStorage fileStorage)
        {
            _context = context;
            _apiService = apiService;
            _userRepository = userRepository;
            //            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckCategoriesAsync();
            await CheckProductsAsync();

            await CheckRolesAsync();
            await CheckCountriesAsync();
            await CheckUserAsync("1010", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "JuanZuluaga.jpeg", UserType.Admin);
            await CheckUserAsync("2020", "Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "LedysBedoya.jpeg", UserType.User);
            await CheckUserAsync("3030", "Brad", "Pitt", "brad@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Brad.jpg", UserType.User);
            await CheckUserAsync("4040", "Angelina", "Jolie", "angelina@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Angelina.jpg", UserType.User);
            await CheckUserAsync("5050", "Bob", "Marley", "bob@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "bob.jpg", UserType.User);
            city = await _context.Cities!.FirstOrDefaultAsync();
            user = await _context.Users!.FirstOrDefaultAsync(x=>x.Cpf_Cnpj == "38713376845");
            await CheckClientsAsync();
            //            client = await _context.Clients!.FirstOrDefaultAsync()!;
            //            await CheckMonthlyFeeAsync();
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
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Cosmeticos E Remedios em geral Teste Nome Grande" });
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
        //        private async Task CheckMonthlyFeeAsync()
        //        {
        //            if(!_context.MonthlyFee.Any())
        //            {
        //                _context.MonthlyFee.Add(new MonthlyFee
        //                {
        //                    Client = client,
        //                    ClientId = client.Id,
        //                    PaymentMethod = PaymentMethod.Dinheiro,
        //                    Reference = DateTime.Now.AddDays(10),
        //                    User = user,
        //                    UserId = user.Id,
        //                    Value = 500,
        //                    DueDate = DateTime.Now.AddDays(10),

        //                });
        //                _context.MonthlyFee.Add(new MonthlyFee
        //                {
        //                    Client = client,
        //                    ClientId = client.Id,
        //                    PaymentMethod = PaymentMethod.Dinheiro,
        //                    Reference = DateTime.Now.AddDays(15),
        //                    Value = 500,
        //                    User = user,
        //                    UserId = user.Id,
        //                    DueDate = DateTime.Now.AddDays(15),

        //                }); _context.MonthlyFee.Add(new MonthlyFee
        //                {
        //                    Client = client,
        //                    ClientId = client.Id,
        //                    Payday = DateTime.Now.AddDays(1),
        //                    PaymentMethod = PaymentMethod.Dinheiro,
        //                    Reference = DateTime.Now.AddDays(7),
        //                    Value = 500,
        //                    DueDate = DateTime.Now.AddDays(7),
        //                    User = user,
        //                    UserId = user.Id,

        //                }); _context.MonthlyFee.Add(new MonthlyFee
        //                {
        //                    Client = client,
        //                    ClientId = client.Id,
        //                    PaymentMethod = PaymentMethod.Dinheiro,
        //                    Reference = DateTime.Now.AddDays(100),
        //                    Value = 500,
        //                    DueDate = DateTime.Now.AddDays(100),
        //                    User = user,
        //                    UserId = user.Id,

        //                }); _context.MonthlyFee.Add(new MonthlyFee
        //                {
        //                    Client = client,
        //                    ClientId = client.Id,
        //                    PaymentMethod = PaymentMethod.Dinheiro,
        //                    Reference = DateTime.Now.AddDays(34),
        //                    Value = 500,
        //                    DueDate = DateTime.Now.AddDays(34),
        //                    User = user,
        //                    UserId = user.Id,

        //                }); _context.MonthlyFee.Add(new MonthlyFee
        //                {
        //                    Client = client,
        //                    ClientId = client.Id,
        //                    PaymentMethod = PaymentMethod.Dinheiro,
        //                    Reference = DateTime.Now.AddDays(200),
        //                    Value = 500,
        //                    DueDate = DateTime.Now.AddDays(200),
        //                    User = user,
        //                    UserId = user.Id,

        //                });
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, ["Calzado", "Deportes"], new List<string>() { "adidas_barracuda.png" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, ["Calzado", "Deportes"], new List<string>() { "Adidas_superstar.png" });
                await AddProductAsync("AirPods", 1300000M, 12F, ["Tecnología", "Apple"], new List<string>() { "airpos.png", "airpos2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, ["Tecnología"], new List<string>() { "audifonos_bose.png" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, ["Deportes"], new List<string>() { "bicicleta_ribble.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, ["Ropa"], new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, ["Deportes"], new List<string>() { "casco_bicicleta.png", "casco.png" });
                await AddProductAsync("iPad", 2300000M, 6F, ["Tecnología", "Apple"], new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, ["Tecnología", "Apple"], new List<string>() { "iphone13.png", "iphone13b.png", "iphone13c.png", "iphone13d.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, ["Tecnología", "Apple"], new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, ["Deportes"], new List<string>() { "mancuernas.png" });
                await AddProductAsync("Mascarilla Cara", 26000M, 100F, ["Belleza"], new List<string>() { "mascarilla_cara.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, ["Calzado", "Deportes"], new List<string>() { "newbalance530.png" });
                await AddProductAsync("New Balance 565", 179000M, 12F, ["Calzado", "Deportes"], new List<string>() { "newbalance565.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, ["Calzado", "Deportes"], new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, ["Calzado", "Deportes"], new List<string>() { "nike_zoom.png" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, ["Ropa", "Deportes"], new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Suplemento Boots Original", 15600M, 12F, ["Nutrición"], new List<string>() { "Boost_Original.png" });
                await AddProductAsync("Whey Protein", 252000M, 12F, ["Nutrición"], new List<string>() { "whey_protein.png" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, ["Mascotas"], new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, ["Mascotas"], new List<string>() { "cama_mascota.png" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, ["Gamer", "Tecnología"], new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, ["Gamer", "Tecnología"], new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, ["Gamer", "Tecnología"], new List<string>() { "mouse_gamer.png" });
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
                //lProductImages = new List<ProductImage>(),
                //User = user,
                //UserId = user.Id,
                IsService = true,
                IsActive = true,

            };

            foreach (var categoryName in categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                if (category != null)
                {
                    prodcut.lProductCategories.Add(new ProductCategory { Category = category });
                }
            }

            //foreach (string? image in images)
            //{
            //    string filePath;
            //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //    {
            //        filePath = $"{Environment.CurrentDirectory}\\Images\\products\\{image}";
            //    }
            //    else
            //    {
            //        filePath = $"{Environment.CurrentDirectory}/Images/products/{image}";
            //    }

            //    var fileBytes = File.ReadAllBytes(filePath);
            //    var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "products");
            //    prodcut.lProductImages.Add(new ProductImage { Image = imagePath });
            //}

            _context.Products.Add(prodcut);
        }


        private async Task CheckClientsAsync()
        {
            //ClientService cli = new();
            //cli.Product = await _context.Products.FirstOrDefaultAsync();

            if (!_context.Clients.Any())
            {
                _context.Clients.Add(new Client
                {
                    Name = "Erenci José Rocha",
                    Cpf_Cnpj = "32299309837",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                    //lClientService = new List<ClientService>()
                    //        {
                    //            cli,
                    //        },
                });
                _context.Clients.Add(new Client
                {
                    Name = "Heloisa Helena da Silva",
                    Cpf_Cnpj = "05599029808",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro dois",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "José do Egito Pereira",
                    Cpf_Cnpj = "01878307460",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro tres",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Jussara Fagundes Rodrigues",
                    Cpf_Cnpj = "27845705845",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro uatro",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Lilian Ferreira de Souza",
                    Cpf_Cnpj = "36481603896",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro cinco",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Luciana Gitti Lopes ",
                    Cpf_Cnpj = "33224077808",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Luciana Oliveira Domingues",
                    Cpf_Cnpj = "25082962876",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Luciana Rocha Luquete De Paiva",
                    Cpf_Cnpj = "25421279804",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Maria Clara Stacco Oliva ",
                    Cpf_Cnpj = "32384951866",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Maria da Penha do Carmo sousa",
                    Cpf_Cnpj = "80693679620",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Maria dos Réus Viana ",
                    Cpf_Cnpj = "13991500817",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Maria Eliana de Souza Leite Galdini ",
                    Cpf_Cnpj = "18023139827",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Noeme Gomes da Silva",
                    Cpf_Cnpj = "96790113304",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Normezia Brandão de Oliveira Almeida",
                    Cpf_Cnpj = "18627482845",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });
                _context.Clients.Add(new Client
                {
                    Name = "Nubia Romeiro Costa",
                    Cpf_Cnpj = "37805830894",
                    MonthlyFeeDueDate = 20,
                    BirthData = DateTime.Now,
                    Neighborhood = "Bairro Um",
                    City = city,
                    AddressNumber = 1,
                    CityId = city.Id,
                    RegisterDate = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                });


                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckRolesAsync()
        {
            await _userRepository.CheckRoleAsync(UserType.Admin.ToString());
            await _userRepository.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userRepository.GetUserAsync(email);
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
                    UserType = userType,
                    CityId = city!.Id,
                };

                await _userRepository.AddUserAsync(user, "123123");
                await _userRepository.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userRepository.GenerateEmailConfirmationTokenAsync(user);
                await _userRepository.ConfirmEmailAsync(user, token);
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
                        if (countryResponse.Name == "Brazil" )// || countryResponse.Name == "Italy")
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
