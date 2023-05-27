using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FleecyBook.Data; // Replace with your own namespace

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Server=DESKTOP-BRPE7R0"),
                b => b.MigrationsAssembly("FleecyBook.Data")));
    }
    
    // Add any other methods you need here
}
