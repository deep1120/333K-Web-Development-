namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenreData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieNumber = c.Int(nullable: false),
                        MovieTitle = c.String(nullable: false),
                        MovieOverview = c.String(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        Tagline = c.String(nullable: false),
                        MPAAratings = c.Int(nullable: false),
                        Actor = c.String(nullable: false),
                        MovieRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        StarRating = c.Int(nullable: false),
                        Customervoting = c.Int(nullable: false),
                        Movie_MovieID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleInitital = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        PopcornPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        Orderstatus = c.Int(nullable: false),
                        Gifted_UserID = c.Int(),
                        Purchased_UserID = c.Int(),
                        User_UserID = c.Int(),
                        User_UserID1 = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Users", t => t.Gifted_UserID)
                .ForeignKey("dbo.Users", t => t.Purchased_UserID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .ForeignKey("dbo.Users", t => t.User_UserID1)
                .Index(t => t.Gifted_UserID)
                .Index(t => t.Purchased_UserID)
                .Index(t => t.User_UserID)
                .Index(t => t.User_UserID1);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketSeat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_OrderID = c.Int(),
                        Showing_ShowingID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.Showings", t => t.Showing_ShowingID)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Showing_ShowingID);
            
            CreateTable(
                "dbo.Showings",
                c => new
                    {
                        ShowingID = c.Int(nullable: false, identity: true),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        RunTime = c.Int(nullable: false),
                        Movie_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ShowingID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .Index(t => t.Movie_MovieID);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        Movie_MovieID = c.Int(nullable: false),
                        Genre_GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieID, t.Genre_GenreID })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.Genre_GenreID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "User_UserID1", "dbo.Users");
            DropForeignKey("dbo.Orders", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Tickets", "Showing_ShowingID", "dbo.Showings");
            DropForeignKey("dbo.Showings", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Tickets", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Purchased_UserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "Gifted_UserID", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.MovieGenres", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.MovieGenres", "Movie_MovieID", "dbo.Movies");
            DropIndex("dbo.MovieGenres", new[] { "Genre_GenreID" });
            DropIndex("dbo.MovieGenres", new[] { "Movie_MovieID" });
            DropIndex("dbo.Showings", new[] { "Movie_MovieID" });
            DropIndex("dbo.Tickets", new[] { "Showing_ShowingID" });
            DropIndex("dbo.Tickets", new[] { "Order_OrderID" });
            DropIndex("dbo.Orders", new[] { "User_UserID1" });
            DropIndex("dbo.Orders", new[] { "User_UserID" });
            DropIndex("dbo.Orders", new[] { "Purchased_UserID" });
            DropIndex("dbo.Orders", new[] { "Gifted_UserID" });
            DropIndex("dbo.Reviews", new[] { "User_UserID" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieID" });
            DropTable("dbo.MovieGenres");
            DropTable("dbo.Showings");
            DropTable("dbo.Tickets");
            DropTable("dbo.Orders");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Movies");
            DropTable("dbo.Genres");
        }
    }
}
