namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "TicketSeat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "TicketSeat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
