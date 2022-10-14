using RegistrationApp.Data;
using RegistrationApp.Services;
using RegistrationApp.Services.Abstraction;
using RegistrationApp.Settings;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.Configure<SqlLiteConnection>(builder.Configuration.GetSection("ConnectionStrings:Default"));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
