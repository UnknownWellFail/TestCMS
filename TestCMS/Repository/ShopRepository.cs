using System;
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
            get { return new NpgsqlConnection(connectionString); }
        }

        public void AddFavorite(int user_id, int shop_id)
        {Console.WriteLine(user_id);
            Console.WriteLine(shop_id);
            using (IDbConnection db = Connection)
            {
                db.Execute("INSERT INTO favorites (user_id, shop_id) VALUES(@user_id, @shop_id)",new {user_id,shop_id});
            }
        }

    public IEnumerable<Favorite> getFavorites(int user_id)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Favorite>("SELECT nickname, avatar_path, name from favorites inner join shops on (favorites.shop_id = shops.id) inner join users on" +
                                          "(favorites.user_id = users.id) where user_id = @user_id",new{user_id});
            }
        }
        
        public IEnumerable<Shop> GetAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Shop>("SELECT * FROM Shops");
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
                return db.Query<Shop>("SELECT * FROM shops WHERE x= @x AND y= @y", new { x,y});
            }

        }
 
        public void Create(Shop shop)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "INSERT INTO shops (name, category, x,y) VALUES(@name, @category, @x,@y)";
                db.Execute(sqlQuery, shop);
            }
        }
 
        public void Update(Shop shop)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "UPDATE shops SET name = @name, category = @category, x = @x, y = @y WHERE id = @id";
                db.Execute(sqlQuery, shop);
            }
        }
 
        public void Delete(int id)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "DELETE FROM shops WHERE id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}