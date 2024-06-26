using System.Runtime.InteropServices;
using Amazon.Models;
using Amazon.Models.Repository;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(System.Environment.OSVersion.ToString());
// Add services to the container.
builder.Services.AddControllersWithViews();

//When using docker
if (System.Environment.OSVersion.ToString().Contains("Unix"))
{
    builder.Services.AddSingleton(new Conexion(builder.Configuration.GetConnectionString("AmazonDocker")));
}
else
{
    builder.Services.AddSingleton(new Conexion(builder.Configuration.GetConnectionString("Amazon")));
}
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Create}");

app.Run();
