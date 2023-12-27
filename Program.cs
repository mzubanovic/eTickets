using AutoMapper;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get the configuration from the appsettings.json file
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Get the connection string from the configuration
var connectionString = configuration.GetConnectionString("DefaultConnectionString");

//DbContext configuration
// DbContext configuration with the connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#region Service Configuration
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IActorsService,ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
#endregion Service Configuration

#region AutoMapper Config
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Movie, NewMovie>()
       .ForMember(dest => dest.ActorIds, opt => opt.MapFrom(src => src.Actors_Movies.Select(m => m.ActorId).ToList()));
});
IMapper editMovieMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(editMovieMapper);
#endregion AutoMapper Config

//Authentication and authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// Add services to the container.
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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed Db
AppDbInitializer.Seed(app);

app.Run();
