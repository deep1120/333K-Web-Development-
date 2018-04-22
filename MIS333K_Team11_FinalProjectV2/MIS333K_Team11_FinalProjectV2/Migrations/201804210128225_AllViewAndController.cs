namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllViewAndController : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderNotes", c => c.String());
            AddColumn("dbo.Showings", "ShowingNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Showings", "ShowingName", c => c.String());
            AddColumn("dbo.Showings", "TicketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Showings", "TicketPrice");
            DropColumn("dbo.Showings", "ShowingName");
            DropColumn("dbo.Showings", "ShowingNumber");
            DropColumn("dbo.Orders", "OrderNotes");
        }
    }
}
