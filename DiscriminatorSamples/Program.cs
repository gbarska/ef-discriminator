using DiscriminatorSamples.Application;
using DiscriminatorSamples.Business;
using DiscriminatorSamples.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var connstring = "Server=(localdb)\\mssqllocaldb;Database=discriminatordb;Trusted_Connection=True;MultipleActiveResultSets=true";

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connstring));

                services.AddScoped<IAuthenticatedUserService, FakeAuthenticatedUserService>();
            })
            .Build();

using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

var dbContext = services.GetRequiredService<AppDbContext>();

var address = new Address()
{
    City = "Orlando",
    State = "Florida",
    Country = "United States"
};

var address2 = new Address()
{
    City = "Orlando",
    State = "Florida",
    Country = "United States"
};

var address3 = new Address()
{
    City = "Orlando",
    State = "Florida",
    Country = "United States"
};

dbContext.ContractEmployees.Add(new ContractEmployee { Name = "John Deere", Charge = 1000m, BonusRate = 1m, Contract = "ASD1234",  Type = EmployeeType.Contract, Address = address });
dbContext.RegisteredEmployees.Add(new RegisteredEmployee { Name = "John Deere", Salary = 1000m, RegistrationNumber = "ASD1234",  Type = EmployeeType.Registered, Address = address2 });
dbContext.Customers.Add(new Customer { Name = "John Deere", Address = address3 });

dbContext.SaveChanges();

Thread.Sleep(1000);

var customers = await dbContext.Customers.ToListAsync();
var contractEmployees = await dbContext.ContractEmployees.ToListAsync();
var registeredEmployees = await dbContext.RegisteredEmployees.ToListAsync();
var addreesses = await dbContext.Addresses.ToListAsync();

Thread.Sleep(1000);
