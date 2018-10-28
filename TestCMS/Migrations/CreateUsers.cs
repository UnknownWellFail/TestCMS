using FluentMigrator;

namespace TestCMS.Migrations
{
    [Migration(1)]
    public class CreateUsers:Migration

    {
        //create table users (id serial primary key, nickname text, avatar_path text, raiting real);

        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity().Indexed()
                .WithColumn("nickname").AsString(50).NotNullable()
                .WithColumn("avatar_path").AsString(50).NotNullable()
                .WithColumn("raiting").AsDouble().NotNullable();
            
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}