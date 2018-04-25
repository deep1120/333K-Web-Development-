namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gotridofquestionmarks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Showings", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Showings", "EndTime", c => c.DateTime());
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime());
        }
    }
}
