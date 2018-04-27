namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeShowing3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Showings", "EndTime");
            DropColumn("dbo.Showings", "RunTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Showings", "RunTime", c => c.Int(nullable: false));
            AddColumn("dbo.Showings", "EndTime", c => c.DateTime(nullable: false));
        }
    }
}
