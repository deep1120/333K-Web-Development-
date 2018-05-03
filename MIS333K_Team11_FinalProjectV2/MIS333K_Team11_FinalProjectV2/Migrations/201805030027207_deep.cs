namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deep : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RatingMovies", "Rating_RatingID", "dbo.Ratings");
            DropForeignKey("dbo.RatingMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "rating_RatingID", "dbo.Ratings");
            DropIndex("dbo.Reviews", new[] { "rating_RatingID" });
            DropIndex("dbo.RatingMovies", new[] { "Rating_RatingID" });
            DropIndex("dbo.RatingMovies", new[] { "Movie_MovieID" });
            DropColumn("dbo.Reviews", "rating_RatingID");
            DropTable("dbo.Ratings");
            DropTable("dbo.RatingMovies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RatingMovies",
                c => new
                    {
                        Rating_RatingID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rating_RatingID, t.Movie_MovieID });
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        RatingScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingID);
            
            AddColumn("dbo.Reviews", "rating_RatingID", c => c.Int());
            CreateIndex("dbo.RatingMovies", "Movie_MovieID");
            CreateIndex("dbo.RatingMovies", "Rating_RatingID");
            CreateIndex("dbo.Reviews", "rating_RatingID");
            AddForeignKey("dbo.Reviews", "rating_RatingID", "dbo.Ratings", "RatingID");
            AddForeignKey("dbo.RatingMovies", "Movie_MovieID", "dbo.Movies", "MovieID", cascadeDelete: true);
            AddForeignKey("dbo.RatingMovies", "Rating_RatingID", "dbo.Ratings", "RatingID", cascadeDelete: true);
        }
    }
}
