namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllOfIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieGenres", newName: "GenreMovies");
            RenameTable(name: "dbo.ShowingMovies", newName: "MovieShowings");
            DropForeignKey("dbo.Orders", "AppUser_Id1", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.GenreMovies");
            DropPrimaryKey("dbo.MovieShowings");
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        AppUserId = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        CardNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUserId)
                .Index(t => t.AppUserId);
            
            AddColumn("dbo.Orders", "AppUser_Id2", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Street", c => c.String(nullable: true));
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime());
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime());
            AddPrimaryKey("dbo.GenreMovies", new[] { "Genre_GenreID", "Movie_MovieID" });
            AddPrimaryKey("dbo.MovieShowings", new[] { "Movie_MovieID", "Showing_ShowingID" });
            CreateIndex("dbo.Orders", "AppUser_Id2");
            AddForeignKey("dbo.Orders", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Orders", "AppUser_Id2", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "AppUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cards", "AppUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "AppUser_Id2" });
            DropIndex("dbo.Cards", new[] { "AppUserId" });
            DropPrimaryKey("dbo.MovieShowings");
            DropPrimaryKey("dbo.GenreMovies");
            AlterColumn("dbo.Showings", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Showings", "ShowDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Street", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.Orders", "AppUser_Id2");
            DropTable("dbo.Cards");
            AddPrimaryKey("dbo.MovieShowings", new[] { "Showing_ShowingID", "Movie_MovieID" });
            AddPrimaryKey("dbo.GenreMovies", new[] { "Movie_MovieID", "Genre_GenreID" });
            AddForeignKey("dbo.Orders", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.MovieShowings", newName: "ShowingMovies");
            RenameTable(name: "dbo.GenreMovies", newName: "MovieGenres");
        }
    }
}
