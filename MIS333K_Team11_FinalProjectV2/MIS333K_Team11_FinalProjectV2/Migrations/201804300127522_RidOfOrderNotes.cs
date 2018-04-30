namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RidOfOrderNotes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "OrderNotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderNotes", c => c.String());
        }
    }
}
