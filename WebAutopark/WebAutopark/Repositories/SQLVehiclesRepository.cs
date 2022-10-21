using Microsoft.EntityFrameworkCore;
using WebAutopark.VehicleLogic;
using Dapper;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace WebAutopark.Repositories
{
    internal class SQLVehiclesRepository : IRepository<Vehicles>
    {
        private string _connectionString = null!;

        public SQLVehiclesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Vehicles item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Vehicles " +
                    "(VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption) " +
                    "VALUES(@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @FuelConsumption)";
                db.Execute(sqlQuery, item);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Vehicles WHERE VehicleId = @VehicleId";
                db.Execute(sqlQuery, new { VehicleId = id });
            }
        }

        public Vehicles? Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Vehicles>("SELECT * FROM Vehicles WHERE VehicleId = @VehicleId", new { VehicleId = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Vehicles> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Vehicles>("SELECT * FROM Vehicles").ToList();
            }
        }

        public void Update(Vehicles item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Vehicles " +
                    "SET VehicleTypeId = @VehicleTypeId, Model = @Model, RegistrationNumber = @RegistrationNumber, " +
                    "Weight = @Weight, Year = @Year, Color = @Color, Mileage = @Mileage, FuelConsumption = @FuelConsumption " +
                    "WHERE VehicleId = @VehicleId";
                db.Execute(sqlQuery, item);
            }
        }
    }
}
