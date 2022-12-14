using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public class SQLOrdersRepository : IRepository<Orders>
    {
        readonly string _connectionString;
        public SQLOrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Create(Orders item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"INSERT INTO Orders
                    (VehicleId, Date)
                    VALUES(@VehicleId, @Date); SELECT CAST(SCOPE_IDENTITY() as int)";
                var asyncResult = await db.QueryAsync<int>(sqlQuery, item);
                item.OrderId = asyncResult.First();
                //item.OrderId = db.QueryAsync<int>(sqlQuery, item).Result.FirstOrDefault();
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Orders WHERE OrderId = @OrderId";
                await db.ExecuteAsync(sqlQuery, new { OrderId = id });
            }
        }

        public async Task<Orders> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<Orders>("SELECT * FROM Orders WHERE OrderId = @OrderId", new { OrderId = id });

            }
        }

        public async Task<IEnumerable<Orders>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Orders>("SELECT * FROM Orders");
            }
        }

        public async Task Update(Orders item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"UPDATE Orders
                    SET VehicleId = @VehicleId, Date = @Date
                    WHERE OrderId = @OrderId";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }
    }
}
