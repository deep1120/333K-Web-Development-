namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsPublishedStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Showings", "IsPublished", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Showings", "IsPublished");
        }
    }
}
