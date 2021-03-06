using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TestCMS.Repository;

namespace TestCMS.Models
{
    public class UserRepository : IRepository<User>
    {
        string connectionString = null;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        internal IDbConnection Connection
        {
            get { return new NpgsqlConnection(connectionString); }
        }

        public IEnumerable<User> GetAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<User>("SELECT * FROM users");
            }
        }

        public User Get(int id)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<User>("SELECT * FROM users WHERE id = @id", new {id}).FirstOrDefault();
            }
        }

        public void Create(User user)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "INSERT INTO users (nickname, avatar_path, raiting) VALUES(@nickname,@avatarpath, @raiting)";
                db.Execute(sqlQuery, user);
            }
        }

        public void Update(User user)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "UPDATE users SET nickname = @nickname, raiting = @raiting WHERE id = @id";
                db.Execute(sqlQuery, user);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = Connection)
            {
                var sqlQuery = "DELETE FROM users WHERE id = @id";
                db.Execute(sqlQuery, new {id});
            }
        }
    }
}