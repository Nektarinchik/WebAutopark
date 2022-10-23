using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public sealed class SQLVehiclesRepository : IRepository<Vehicles>
    {
        private string _connectionString = null!;

        public SQLVehiclesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task Create(Vehicles item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Vehicles " +
                    "(VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption) " +
                    "VALUES(@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @FuelConsumption)";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }
        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Vehicles WHERE VehicleId = @VehicleId";
                await db.ExecuteAsync(sqlQuery, new { VehicleId = id });
            }
        }

        public async Task<Vehicles> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<Vehicles>("SELECT * FROM Vehicles WHERE VehicleId = @VehicleId", new { VehicleId = id });
            }
        }

        public async Task<IEnumerable<Vehicles>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Vehicles>("SELECT * FROM Vehicles");
            }
        }

        public async Task Update(Vehicles item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Vehicles " +
                    "SET VehicleTypeId = @VehicleTypeId, Model = @Model, RegistrationNumber = @RegistrationNumber, " +
                    "Weight = @Weight, Year = @Year, Color = @Color, Mileage = @Mileage, FuelConsumption = @FuelConsumption " +
                    "WHERE VehicleId = @VehicleId";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }
    }
}
