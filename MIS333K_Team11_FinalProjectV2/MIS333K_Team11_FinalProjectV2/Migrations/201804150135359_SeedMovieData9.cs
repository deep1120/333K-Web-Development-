namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMovieData9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "MovieTitle", c => c.String());
            AlterColumn("dbo.Movies", "MovieOverview", c => c.String());
            AlterColumn("dbo.Movies", "Actor", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Actor", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "MovieOverview", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "MovieTitle", c => c.String(nullable: false));
        }
    }
}
