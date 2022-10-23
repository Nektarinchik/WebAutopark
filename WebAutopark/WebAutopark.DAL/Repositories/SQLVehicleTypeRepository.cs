using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public sealed class SQLVehicleTypeRepository : IRepository<VehicleTypes>
    {
        private string _connectionString = null!;
        public SQLVehicleTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task Create(VehicleTypes item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO VehicleTypes (Name, TaxCoefficient) VALUES(@Name, @TaxCoefficient)";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @VehicleTypeId";
                await db.ExecuteAsync(sqlQuery, new { VehicleTypeId = id });
            }
        }

        public async Task<VehicleTypes> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<VehicleTypes>("SELECT * FROM VehicleTypes WHERE VehicleTypeId = @VehicleTypeId", new { VehicleTypeId = id });
            }
        }

        public async Task<IEnumerable<VehicleTypes>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<VehicleTypes>("SELECT * FROM VehicleTypes");
            }
        }
        public async Task Update(VehicleTypes item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE VehicleTypes " +
                    "SET Name = @Name, TaxCoefficient = @TaxCoefficient " +
                    "WHERE VehicleTypeId = @VehicleTypeId";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }
    }
}
