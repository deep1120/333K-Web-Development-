namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Identity2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "CardNumber");
            DropColumn("dbo.Orders", "GiftEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "GiftEmail", c => c.String());
            AddColumn("dbo.Orders", "CardNumber", c => c.String());
        }
    }
}
