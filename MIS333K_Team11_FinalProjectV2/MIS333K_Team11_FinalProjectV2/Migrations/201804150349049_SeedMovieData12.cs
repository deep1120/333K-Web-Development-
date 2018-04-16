namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMovieData12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "Customervoting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "Customervoting", c => c.Int(nullable: false));
        }
    }
}
