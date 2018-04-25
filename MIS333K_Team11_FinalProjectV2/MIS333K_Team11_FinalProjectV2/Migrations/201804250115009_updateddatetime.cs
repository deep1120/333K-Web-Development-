namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime());
        }
    }
}
