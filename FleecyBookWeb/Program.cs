using FleecyBook.DataAccess;
using FleecyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//_add service for dbcontext & sqlserver & constr
// All are merger at this point                      install second package M.EFC.SqlServer        _4        
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Conio")));   //pass the ConStr name it'll auto find & config
                                                            //our sql server

//after this, use Migration..in commands.. _5 (3rd pkg M>EFC>Tools) ..for physically database 

//_To instinct reload the page, on changing code 👇
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // when using Repository

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
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
