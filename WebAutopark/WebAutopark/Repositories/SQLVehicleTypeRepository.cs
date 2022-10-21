using Microsoft.EntityFrameworkCore;
using WebAutopark.VehicleLogic;
using Dapper;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace WebAutopark.Repositories
{
    public sealed class SQLVehicleTypeRepository : IRepository<VehicleTypes>
    {
        private string _connectionString = null!;
        public SQLVehicleTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(VehicleTypes item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO VehicleTypes (Name, TaxCoefficient) VALUES(@Name, @TaxCoefficient)";
                db.Execute(sqlQuery, item);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @VehicleTypeId";
                db.Execute(sqlQuery, new { VehicleTypeId = id });
            }
        }

        public VehicleTypes? Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<VehicleTypes>("SELECT * FROM VehicleTypes WHERE VehicleTypeId = @VehicleTypeId", new { VehicleTypeId = id }).FirstOrDefault();
            }
        }

        public IEnumerable<VehicleTypes> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<VehicleTypes>("SELECT * FROM VehicleTypes").ToList();
            }
        }
        public void Update(VehicleTypes item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE VehicleTypes " +
                    "SET Name = @Name, TaxCoefficient = @TaxCoefficient " +
                    "WHERE VehicleTypeId = @VehicleTypeId";
                db.Execute(sqlQuery, item);
            }
        }

        //private VehicleTypeContext _db;
        //private bool _disposed = false;
        //public SQLVehicleTypeRepository(VehicleTypeContext db)
        //{
        //    _db = db;
        //}
        //public void Create(VehicleTypes item)
        //{
        //    _db.VehicleTypes.Add(item);
        //}
        //public void Delete(int id)
        //{
        //    VehicleTypes? vehicleType = _db.VehicleTypes.Find(id);
        //    if (vehicleType != null)
        //    {
        //        _db.VehicleTypes.Remove(vehicleType);
        //    }
        //}
        //public void Dispose()
        //{
        //    if (!_disposed)
        //    {
        //        _db.Dispose();
        //    }
        //    _disposed = true;
        //    GC.SuppressFinalize(this);
        //}
        //public VehicleTypes? Get(int id)
        //{
        //    return _db.VehicleTypes.Find(id);
        //}
        //public IEnumerable<VehicleTypes> GetAll()
        //{
        //    return _db.VehicleTypes;
        //}
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}
        //public void Update(VehicleTypes item)
        //{
        //    _db.Entry(item).State = EntityState.Modified;
        //}

    }
}
