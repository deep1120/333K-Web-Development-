namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class giftemail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "GiftEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "GiftEmail");
        }
    }
}
