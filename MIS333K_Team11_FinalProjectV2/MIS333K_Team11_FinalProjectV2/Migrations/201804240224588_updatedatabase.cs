namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MiddleInitital", c => c.String());
            DropColumn("dbo.AspNetUsers", "MiddleInitial");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MiddleInitial", c => c.String());
            DropColumn("dbo.AspNetUsers", "MiddleInitital");
        }
    }
}