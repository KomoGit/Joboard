using FluentMigrator;

namespace Infrastructure.Migrations.Migrations
{
    [Migration(2)]
    public class DatabaseMigration_2 : Migration
    {
        public override void Up()
        {
            Create.Schema("client_side");

            Create.Table("jobs")
                .InSchema("client_side")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("jobs");
        }
    }
}