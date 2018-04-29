namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixAllUserID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserID", c => c.String());
        }
    }
}
