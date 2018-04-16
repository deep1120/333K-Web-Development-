namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMovieData8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Tagline", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Tagline", c => c.String(nullable: false));
        }
    }
}
