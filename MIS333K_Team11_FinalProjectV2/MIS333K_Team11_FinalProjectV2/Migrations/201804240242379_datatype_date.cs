namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatype_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MiddleInitital", c => c.String());
            AddColumn("dbo.Showings", "ShowDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "MiddleInitial");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MiddleInitial", c => c.String());
            DropColumn("dbo.Showings", "ShowDate");
            DropColumn("dbo.AspNetUsers", "MiddleInitital");
        }
    }
}
