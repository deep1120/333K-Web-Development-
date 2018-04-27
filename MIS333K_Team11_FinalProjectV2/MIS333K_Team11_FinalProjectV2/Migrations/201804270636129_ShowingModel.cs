namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowingModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "Showing_ShowingID", "dbo.Showings");
            DropIndex("dbo.Seats", new[] { "Showing_ShowingID" });
            AddColumn("dbo.Showings", "EndTime", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ZipCode", c => c.String(nullable: false));
            DropColumn("dbo.Showings", "IsPublished");
            DropTable("dbo.Seats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatName = c.String(),
                        Showing_ShowingID = c.Int(),
                    })
                .PrimaryKey(t => t.SeatID);
            
            AddColumn("dbo.Showings", "IsPublished", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ZipCode", c => c.String());
            AlterColumn("dbo.AspNetUsers", "State", c => c.String());
            AlterColumn("dbo.AspNetUsers", "City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Street", c => c.String());
            DropColumn("dbo.Showings", "EndTime");
            CreateIndex("dbo.Seats", "Showing_ShowingID");
            AddForeignKey("dbo.Seats", "Showing_ShowingID", "dbo.Showings", "ShowingID");
        }
    }
}
