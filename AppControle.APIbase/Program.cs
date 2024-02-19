using AppControle.API.Data;
using AppControle.API.Helpers;




//Azure
builder.Services.AddScoped<IFileStorage, FileStorage>();

//API
builder.Services.AddScoped<IApiService, ApiService>();
//Orders
//builder.Services.AddScoped<IOrdersHelper, OrdersHelper>();
//Auth
builder.Services.AddScoped<IUserHelper, UserHelper>();




