namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime());
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime(nullable: false));
        }
    }
}
