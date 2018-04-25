namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEmailAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CardNumber", c => c.String());
            AddColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Total");
            DropColumn("dbo.Orders", "CardNumber");
        }
    }
}
