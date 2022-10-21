using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using WebAutopark.Repositories;
using WebAutopark.VehicleLogic;

string connectionString = @"Data Source=DESKTOP-G9GG5EN; Encrypt=False; Integrated Security=false; Initial Catalog=Autopark; User ID=nikita;Password=nikita;";

string script = File.ReadAllText(@"Scripts\CreateTablesAutoparkDBScript.sql");
using (IDbConnection db = new SqlConnection(connectionString))
{
    db.Execute(script);
}
TEST VehicleTypes

SQLVehicleTypeRepository repVehicleTypes = new SQLVehicleTypeRepository(connectionString);
VehicleTypes vt = new VehicleTypes(1, "passenger", 1.0);
repVehicleTypes.Create(vt);
vt.TaxCoefficient = 1.2;
repVehicleTypes.Update(vt);
VehicleTypes? vtTest = repVehicleTypes.Get(vt.VehicleTypeId);
foreach (var item in repVehicleTypes.GetAll())
{
    Console.WriteLine(item.Name);
}

// TEST Vehicles

SQLVehiclesRepository repVehicles = new SQLVehiclesRepository(connectionString);
Vehicles vehicle = new Vehicles(1, 1, "Mercedes W211", "6582 EP-2", 2000.0,
    2009, 300.000, WebAutopark.VehicleLogic.Colors.GRAY, 11.0
    );
repVehicles.Create(vehicle);
vehicle.Mileage += 50000;
repVehicles.Update(vehicle);
Vehicles? vehicleTest = repVehicles.Get(vehicle.VehicleId);
foreach (var item in repVehicles.GetAll())
{
    Console.WriteLine(item.Model);
}
repVehicles.Delete(vehicle.VehicleId);
repVehicles.Create(vehicle);

//TEST Components

SQLComponentsRepository repComponents = new SQLComponentsRepository(connectionString);
repComponents.Create(new WebAutopark.VehicleLogic.Components(1, "Втулка"));
Components component = new Components(2, "ГРМ");
repComponents.Create(component);
component.Name = "Масло";
repComponents.Update(component);
Components? componentTest = repComponents.Get(component.ComponentId);
foreach (var item in repComponents.GetAll())
{
    Console.WriteLine(item.Name);
}
