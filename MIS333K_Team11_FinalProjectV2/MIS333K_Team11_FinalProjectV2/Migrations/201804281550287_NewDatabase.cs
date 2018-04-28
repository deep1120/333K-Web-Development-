namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieShowings", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.MovieShowings", "Showing_ShowingID", "dbo.Showings");
            DropIndex("dbo.MovieShowings", new[] { "Movie_MovieID" });
            DropIndex("dbo.MovieShowings", new[] { "Showing_ShowingID" });
            AddColumn("dbo.Showings", "SponsoringMovie_MovieID", c => c.Int());
            CreateIndex("dbo.Showings", "SponsoringMovie_MovieID");
            AddForeignKey("dbo.Showings", "SponsoringMovie_MovieID", "dbo.Movies", "MovieID");
            DropColumn("dbo.Showings", "EndTime");
            DropColumn("dbo.Showings", "RunTime");
            DropTable("dbo.MovieShowings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieShowings",
                c => new
                    {
                        Movie_MovieID = c.Int(nullable: false),
                        Showing_ShowingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieID, t.Showing_ShowingID });
            
            AddColumn("dbo.Showings", "RunTime", c => c.Int(nullable: false));
            AddColumn("dbo.Showings", "EndTime", c => c.DateTime());
            DropForeignKey("dbo.Showings", "SponsoringMovie_MovieID", "dbo.Movies");
            DropIndex("dbo.Showings", new[] { "SponsoringMovie_MovieID" });
            DropColumn("dbo.Showings", "SponsoringMovie_MovieID");
            CreateIndex("dbo.MovieShowings", "Showing_ShowingID");
            CreateIndex("dbo.MovieShowings", "Movie_MovieID");
            AddForeignKey("dbo.MovieShowings", "Showing_ShowingID", "dbo.Showings", "ShowingID", cascadeDelete: true);
            AddForeignKey("dbo.MovieShowings", "Movie_MovieID", "dbo.Movies", "MovieID", cascadeDelete: true);
        }
    }
}
