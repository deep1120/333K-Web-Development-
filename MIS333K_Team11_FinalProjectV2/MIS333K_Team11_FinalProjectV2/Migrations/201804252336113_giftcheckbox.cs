namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class giftcheckbox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Gift", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Gift");
        }
    }
}
