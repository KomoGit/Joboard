using FluentMigrator;

namespace Infrastructure.Migrations.Migrations
{
    [Migration(1, "Category table created")]
    public class InitialMigration_1 : ForwardOnlyMigration
    {
        public override void Up() 
        {
            //for running a custom sql, you can choose Execute.Sql method 
            Execute.Sql(@"CREATE TABLE public.""Categories"" 
                          (
                          	""Id"" uuid NOT NULL,
                          	""Name"" text NOT NULL,
                          	""Description"" text NULL,
                          	CONSTRAINT ""Categories_pkey"" PRIMARY KEY (""Id"")
                          );");
            //Execute.EmbeddedScript("FluentMigratorDemo.Scripts.InsertCategories.sql");
        } 
    }
}
