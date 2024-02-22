using AppControle.API.Data;
using AppControle.API.Helpers;




//Azure
builder.Services.AddScoped<IFileStorage, FileStorage>();


//Orders
//builder.Services.AddScoped<IOrdersHelper, OrdersHelper>();
//Auth
builder.Services.AddScoped<IUserHelper, UserHelper>();




