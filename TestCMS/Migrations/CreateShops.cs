using FluentMigrator;

namespace TestCMS.Migrations
{
    [Migration(2)]
    public class CreateShops:Migration
    {
        //create table shops (id serial primary key, name text, category text, x real, y real);

        public override void Up()
        {
            Create.Table("shops")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity().Indexed()
                .WithColumn("name").AsString(50).NotNullable()
                .WithColumn("category").AsString(50).NotNullable()
                .WithColumn("x").AsDouble().NotNullable()
                .WithColumn("y").AsDouble().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("shops");
        }
    }
}