using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;


namespace WebAutopark.DAL.Repositories
{
    public sealed class SQLComponentsRepository : IRepository<Components>
    {
        readonly string _connectionString;
        public SQLComponentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Create(Components item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"INSERT INTO Components 
                    (Name) 
                    VALUES(@Name)";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Components WHERE ComponentId = @ComponentId";
                await db.ExecuteAsync(sqlQuery, new { ComponentId = id });
            }
        }

        public async Task<Components> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<Components>("SELECT * FROM Components WHERE ComponentId = @ComponentId", new { ComponentId = id });
            }
        }

        public async Task<IEnumerable<Components>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Components>("SELECT * FROM Components");
            }
        }

        public async Task Update(Components item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"UPDATE Components
                    SET Name = @Name
                    WHERE ComponentId = @ComponentId";
                await db.ExecuteAsync(sqlQuery, item);
            }
        }
    }
}
