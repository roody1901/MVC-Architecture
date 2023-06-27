using BankingSystem.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAdminRepository, AdminRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapGet("/hello", () => {
    return new List<string>() {
        "Hello World!",
        "Hello Galaxy!",
        "Hello Universe!"
    };
});

app.MapDefaultControllerRoute();

app.Run();