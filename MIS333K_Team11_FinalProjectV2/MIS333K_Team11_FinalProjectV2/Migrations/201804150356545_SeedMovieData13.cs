namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMovieData13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ZipCode", c => c.String());
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Showings", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Showings", "EndTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Showings", "StartTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "ZipCode", c => c.Int(nullable: false));
        }
    }
}
