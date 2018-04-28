namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_datetime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Showings", "StartTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Showings", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
