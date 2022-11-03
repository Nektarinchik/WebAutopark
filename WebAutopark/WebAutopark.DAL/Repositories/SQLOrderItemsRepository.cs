using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public class SQLOrderItemsRepository : IRepository<OrderItems>
    {
        private string _connectionString = null!;
        public SQLOrderItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task Create(OrderItems item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Components " +
                    "(OrderId, ComponentId, Quantity) " +
                    "VALUES(@OrderId, @ComponentId, @Quantity)";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM OrderItems WHERE OrderItemId = @OrderItemId";
                await db.ExecuteAsync(sqlQuery, new { OrderItemId = id });
            }
        }

        public async Task<OrderItems> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<OrderItems>("SELECT * FROM Components WHERE OrderItemId = @OrderItemId",
                    new { OrderItemId = id });
            }
        }

        public async Task<IEnumerable<OrderItems>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<OrderItems>("SELECT * FROM OrderItems");
            }
        }

        public async Task Update(OrderItems item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE OrderItems " +
                    "SET OrderId = @OrderId, ComponentId = @ComponentId, Quantity = @Quantity" +
                    "WHERE OrderItemId = @OrderItemId";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }

        public async Task<OrderItems> GetInstanceByOrderId(int orderId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<OrderItems>("SELECT * FROM OrderItems WHERE OrderId = @OrderId",
                    new { OrderId = orderId });
            }
        }
    }
}
