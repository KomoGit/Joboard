using Domain.Entities;
using FluentMigrator;

namespace Infrastructure.Migrations.Migrations
{
    [Migration(1, "Initial tables and schema made.")]
    public class InitialMigration_1 : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Schema("client_side");

            Create.Table("jobs")
                .InSchema("client_side")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable();

            Create.Table(typeof(CategoryEntity).Name)
                .InSchema("client_side");
        }
    }
}
