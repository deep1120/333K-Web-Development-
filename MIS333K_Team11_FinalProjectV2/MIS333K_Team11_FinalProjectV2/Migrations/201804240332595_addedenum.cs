namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedenum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Showings", "Theatre", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Showings", "Theatre");
        }
    }
}
