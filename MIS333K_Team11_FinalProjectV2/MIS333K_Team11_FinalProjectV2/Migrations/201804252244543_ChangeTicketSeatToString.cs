namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTicketSeatToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "TicketSeat", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "TicketSeat", c => c.Int(nullable: false));
        }
    }
}
