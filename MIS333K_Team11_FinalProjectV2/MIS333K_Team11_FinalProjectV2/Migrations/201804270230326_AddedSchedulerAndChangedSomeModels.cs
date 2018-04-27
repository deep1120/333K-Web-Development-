namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSchedulerAndChangedSomeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatName = c.String(),
                        Showing_ShowingID = c.Int(),
                    })
                .PrimaryKey(t => t.SeatID)
                .ForeignKey("dbo.Showings", t => t.Showing_ShowingID)
                .Index(t => t.Showing_ShowingID);
            
            AddColumn("dbo.Showings", "IsPublished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seats", "Showing_ShowingID", "dbo.Showings");
            DropIndex("dbo.Seats", new[] { "Showing_ShowingID" });
            DropColumn("dbo.Showings", "IsPublished");
            DropTable("dbo.Seats");
        }
    }
}
