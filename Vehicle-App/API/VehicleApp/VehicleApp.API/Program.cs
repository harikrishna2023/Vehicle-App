
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using VehicleApp.API.Data;
using VehicleApp.API.Mappings;
using VehicleApp.API.Repositories;
using VehicleApp.API.Repositories.IRepositories;

namespace VehicleApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().
                 AllowAnyHeader());
            });

            

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<VehicleAppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleAppConnectionString")));

            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddScoped<ICategoryRepository, CategoryRepositorycs>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            var app = builder.Build();

            app.UseCors("AllowOrigin");
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"CategoryIcons")),
                RequestPath = new PathString("/CategoryIcons")
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"CategoryIconsTemp")),
                RequestPath = new PathString("/CategoryIconsTemp")
            });


            app.MapControllers();

            app.Run();
        }
    }
}