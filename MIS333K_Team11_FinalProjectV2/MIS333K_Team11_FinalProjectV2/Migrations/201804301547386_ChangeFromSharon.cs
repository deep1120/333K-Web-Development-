
namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFromSharon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.Tickets", new[] { "Order_OrderID" });
            RenameColumn(table: "dbo.Tickets", name: "Order_OrderID", newName: "OrderID");
            RenameColumn(table: "dbo.Reviews", name: "Movie_MovieID", newName: "MovieReview_MovieID");
            RenameIndex(table: "dbo.Reviews", name: "IX_Movie_MovieID", newName: "IX_MovieReview_MovieID");
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        RatingScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingID);
            
            CreateTable(
                "dbo.RatingMovies",
                c => new
                    {
                        Rating_RatingID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rating_RatingID, t.Movie_MovieID })
                .ForeignKey("dbo.Ratings", t => t.Rating_RatingID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.Rating_RatingID)
                .Index(t => t.Movie_MovieID);
            
            AddColumn("dbo.Tickets", "MovieID", c => c.Int());
            AddColumn("dbo.Movies", "FeaturedSong", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reviews", "Comment", c => c.String(maxLength: 100));
            AddColumn("dbo.Reviews", "rating_RatingID", c => c.Int());
            AlterColumn("dbo.Tickets", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "OrderID");
            CreateIndex("dbo.Reviews", "rating_RatingID");
            AddForeignKey("dbo.Reviews", "rating_RatingID", "dbo.Ratings", "RatingID");
            AddForeignKey("dbo.Tickets", "OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Reviews", "rating_RatingID", "dbo.Ratings");
            DropForeignKey("dbo.RatingMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.RatingMovies", "Rating_RatingID", "dbo.Ratings");
            DropIndex("dbo.RatingMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.RatingMovies", new[] { "Rating_RatingID" });
            DropIndex("dbo.Reviews", new[] { "rating_RatingID" });
            DropIndex("dbo.Tickets", new[] { "OrderID" });
            AlterColumn("dbo.Tickets", "OrderID", c => c.Int());
            DropColumn("dbo.Reviews", "rating_RatingID");
            DropColumn("dbo.Reviews", "Comment");
            DropColumn("dbo.Movies", "FeaturedSong");
            DropColumn("dbo.Tickets", "MovieID");
            DropTable("dbo.RatingMovies");
            DropTable("dbo.Ratings");
            RenameIndex(table: "dbo.Reviews", name: "IX_MovieReview_MovieID", newName: "IX_Movie_MovieID");
            RenameColumn(table: "dbo.Reviews", name: "MovieReview_MovieID", newName: "Movie_MovieID");
            RenameColumn(table: "dbo.Tickets", name: "OrderID", newName: "Order_OrderID");
            CreateIndex("dbo.Tickets", "Order_OrderID");
            AddForeignKey("dbo.Tickets", "Order_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
