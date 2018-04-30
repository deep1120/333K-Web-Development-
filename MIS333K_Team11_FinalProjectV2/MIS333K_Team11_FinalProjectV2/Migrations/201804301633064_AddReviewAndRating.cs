namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewAndRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "Subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Movies", "FeaturedMovie", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Tickets", "MovieID");
            AddForeignKey("dbo.Tickets", "MovieID", "dbo.Movies", "MovieID");
            DropColumn("dbo.Movies", "FeaturedSong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "FeaturedSong", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Tickets", "MovieID", "dbo.Movies");
            DropIndex("dbo.Tickets", new[] { "MovieID" });
            DropColumn("dbo.Movies", "FeaturedMovie");
            DropColumn("dbo.Tickets", "Subtotal");
            DropColumn("dbo.Tickets", "Quantity");
        }
    }
}
