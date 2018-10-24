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

        public IEnumerable<Shop> Get(string category)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Shop>("SELECT * FROM Shops WHERE category= @category", new { category});
            }

        }
        
        public IEnumerable<Shop> Get(double x, double y)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Shop>("SELECT * FROM Shops WHERE x= @x AND y= @y", new { x,y});
            }

        }
 
        public void Create(Shop shop)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "INSERT INTO Shops (Name, category, x,y) VALUES(@Name, @Category, @X,@Y)";
                db.Execute(sqlQuery, shop);
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