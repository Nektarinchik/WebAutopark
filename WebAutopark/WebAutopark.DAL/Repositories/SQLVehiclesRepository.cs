using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public sealed class SQLVehiclesRepository : IRepository<Vehicles>
    {
        private string _connectionString = null!; //no need to initialize here, make it readonly

        public SQLVehiclesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task Create(Vehicles item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Vehicles " + //we can use '@' instead of concatenation
                    "(VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption, Volume) " +
                    "VALUES(@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @FuelConsumption, @Volume)";
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
                string sqlQuery = "UPDATE Vehicles " + //we can use '@' instead of concatenation
                    "SET VehicleTypeId = @VehicleTypeId, Model = @Model, RegistrationNumber = @RegistrationNumber, " +
                    "Weight = @Weight, Year = @Year, Color = @Color, Mileage = @Mileage, FuelConsumption = @FuelConsumption, Volume = @Volume " +
                    "WHERE VehicleId = @VehicleId";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }
        public async Task<IEnumerable<Vehicles>> GetSortedByModel()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Vehicles>("SELECT * FROM Vehicles ORDER BY Model");
            }
        }
        public async Task<IEnumerable<Vehicles>> GetSortedByMileage()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Vehicles>("SELECT * FROM Vehicles ORDER BY Mileage");
            }
        }
        public async Task<IEnumerable<Vehicles>> GetSortedByVehicleType()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Vehicles>("SELECT * FROM Vehicles AS v " + //we can use '@' instead of concatenation
                    "INNER JOIN VehicleTypes AS vt " +
                    "ON v.VehicleTypeId = vt.VehicleTypeId " +
                    "ORDER BY vt.Name");
            }
        }
    }
}
