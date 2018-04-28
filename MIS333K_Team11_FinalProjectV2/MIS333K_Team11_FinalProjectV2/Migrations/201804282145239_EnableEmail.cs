namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserID");
        }
    }
}
