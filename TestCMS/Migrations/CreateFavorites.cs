using System.Data;
using FluentMigrator;

namespace TestCMS.Migrations
{
    [Migration(3)]
    public class CreateFavorites : Migration
    {
        //create table favorites (user_id int NOT NULL REFERENCES users(id) ON DELETE CASCADE, shop_id int not null REFERENCES shops(id) ON DELETE CASCADE, primary key(user_id,shop_id));
        public override void Up()
        {
            Create.Table("favorites")
                .WithColumn("users_id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("shops_id").AsInt32().PrimaryKey().NotNullable();
            Create.ForeignKey().FromTable("favorites").ForeignColumn("users_id").ToTable("users").PrimaryColumn("id").OnDelete(Rule.Cascade);
            Create.ForeignKey().FromTable("favorites").ForeignColumn("shops_id").ToTable("shops").PrimaryColumn("id").OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey("users_id");
            Delete.ForeignKey("shops_id");
            Delete.Table("favorites");
        }
    }
}