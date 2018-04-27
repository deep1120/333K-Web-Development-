namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderReviewShowingModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Street", c => c.String());
            AlterColumn("dbo.AspNetUsers", "City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "ZipCode", c => c.String());
            DropColumn("dbo.Showings", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Showings", "EndTime", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Street", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "State");
        }
    }
}
