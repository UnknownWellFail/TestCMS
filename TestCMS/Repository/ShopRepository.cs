using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TestCMS.Repository;

namespace TestCMS.Models
{

    
    public class ShopRepository : IRepository<Shop>
    {
        string connectionString = null;
        public ShopRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
        
        public List<Shop> GetAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Shop>("SELECT * FROM Shops").ToList();
            }
        }
 
        public Shop Get(int id)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Shop>("SELECT * FROM Shops WHERE id = @id", new { id }).FirstOrDefault();
            }
        }
 
        public void Create(Shop shop)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "INSERT INTO Shops (Name, category, x,y) VALUES(@Name, @Category, @X,@Y)";
                db.Execute(sqlQuery, shop);
 
                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }
 
        public void Update(Shop shop)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "UPDATE Shops SET Name = @Name, Age = @Age WHERE Id = @Id";
                db.Execute(sqlQuery, shop);
            }
        }
 
        public void Delete(int id)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "DELETE FROM Shops WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}