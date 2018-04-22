namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedoShowing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Showings", "Movie_MovieID", "dbo.Movies");
            DropIndex("dbo.Showings", new[] { "Movie_MovieID" });
            CreateTable(
                "dbo.ShowingMovies",
                c => new
                    {
                        Showing_ShowingID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Showing_ShowingID, t.Movie_MovieID })
                .ForeignKey("dbo.Showings", t => t.Showing_ShowingID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.Showing_ShowingID)
                .Index(t => t.Movie_MovieID);
            
            DropColumn("dbo.Showings", "Movie_MovieID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Showings", "Movie_MovieID", c => c.Int());
            DropForeignKey("dbo.ShowingMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.ShowingMovies", "Showing_ShowingID", "dbo.Showings");
            DropIndex("dbo.ShowingMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.ShowingMovies", new[] { "Showing_ShowingID" });
            DropTable("dbo.ShowingMovies");
            CreateIndex("dbo.Showings", "Movie_MovieID");
            AddForeignKey("dbo.Showings", "Movie_MovieID", "dbo.Movies", "MovieID");
        }
    }
}
