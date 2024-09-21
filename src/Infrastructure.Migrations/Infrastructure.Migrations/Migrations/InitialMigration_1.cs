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

            Create.Table("job")
                .InSchema("client_side")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("description").AsString(3000).NotNullable()
                .WithColumn("views").AsInt32().WithDefaultValue(0)
                .WithColumn("expiration_date").AsDateTime().WithDefaultValue(DateTime.UtcNow.AddDays(14))
                .WithColumn("create_date").AsDateTime().WithDefaultValue(DateTime.UtcNow)
                .WithColumn("update_date").AsDateTime().WithDefaultValue(DateTime.UtcNow);
        }
    }
}