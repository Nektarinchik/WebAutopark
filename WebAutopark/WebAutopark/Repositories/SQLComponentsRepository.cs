using Microsoft.EntityFrameworkCore;
using WebAutopark.VehicleLogic;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace WebAutopark.Repositories
{
    public sealed class SQLComponentsRepository : IRepository<Components>
    {
        private string _connectionString = null!;
        public SQLComponentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Components item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Components " +
                    "(Name) " +
                    "VALUES(@Name)";
                db.Execute(sqlQuery, item);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Components WHERE ComponentId = @ComponentId";
                db.Execute(sqlQuery, new { ComponentId = id });
            }
        }

        public Components? Get(int id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Components>("SELECT * FROM Components WHERE ComponentId = @ComponentId", new { ComponentId = id }).FirstOrDefault();

            }    
        }

        public IEnumerable<Components> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Components>("SELECT * FROM Components");
            }
        }

        public void Update(Components item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Components " +
                    "SET Name = @Name " +
                    "WHERE ComponentId = @ComponentId";
                db.Execute(sqlQuery, item);
            }
        }
    }
}
