using Microsoft.EntityFrameworkCore;
using Proyecto_Restaurante.Data;
using Proyecto_Restaurante.Services;
using Proyecto_Restaurante.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var stripeSecret = builder.Configuration["Stripe:SecretKey"]
                   ?? Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");

// Validación temprana y bloqueo del arranque si la clave es inválida o placeholder
if (string.IsNullOrWhiteSpace(stripeSecret)
    || !stripeSecret.StartsWith("sk_")
    || stripeSecret.Contains("XXXXXXXX"))
{
    throw new InvalidOperationException("Stripe Secret Key inválida o no configurada. En desarrollo usa: dotnet user-secrets set \"Stripe:SecretKey\" \"sk_test_...\". En producción configura la variable de entorno STRIPE_SECRET_KEY.");
}

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RestauranteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sql => sql.EnableRetryOnFailure()
    ));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRestauranteService, RestauranteService>();
builder.Services.AddScoped<IPlatilloService, PlatilloService>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
