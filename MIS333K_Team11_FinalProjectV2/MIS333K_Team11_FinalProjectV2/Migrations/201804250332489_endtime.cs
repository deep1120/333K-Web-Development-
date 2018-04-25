namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endtime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime());
        }
    }
}
