using Customers.Service.Infrastructure;
using Customers.Service.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Customers.Service
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      //Add PostgreSQL support
      //services.AddDbContext<CustomersDbContext>(options => {
      //    options.UseNpgsql(Configuration.GetConnectionString("CustomersPostgresConnectionString"));
      //});

      //Add SQL Server support
      //services.AddDbContext<CustomersDbContext>(options => {
      //    options.UseSqlServer(Configuration.GetConnectionString("CustomersSqlServerConnectionString"));
      //});

      //Add SqLite support
      services.AddDbContext<CustomersDbContext>(options =>
      {
        options.UseSqlite(Configuration.GetConnectionString("CustomersSqliteConnectionString"));
      });

      services.AddControllers();

      //https://github.com/domaindrivendev/Swashbuckle.AspNetCore
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Application API",
          Description = "Application Documentation",
          Contact = new OpenApiContact { Name = "Author" },
          License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://en.wikipedia.org/wiki/MIT_License") }
        });
      });

      services.AddScoped<IStatesRepository, StatesRepository>();
      services.AddScoped<ICustomersRepository, CustomersRepository>();
      services.AddTransient<CustomersDbSeeder>();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    }

    public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env,
        CustomersDbSeeder customersDbSeeder)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      // Visit https://localhost:5000/swagger
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });


      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
    }
  }
}
