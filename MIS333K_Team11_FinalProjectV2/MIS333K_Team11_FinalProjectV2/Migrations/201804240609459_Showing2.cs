namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Showing2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Showings", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Showings", "EndTime", c => c.DateTime(nullable: false));
        }
    }
}
