namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NEW : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        MiddleInitial = c.String(),
                        LastName = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        PopcornPoints = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        ConfirmationNumber = c.Int(nullable: false),
                        EarlyDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SeniorDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                        CardNumber = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orderstatus = c.Int(nullable: false),
                        Gift = c.Boolean(nullable: false),
                        GiftEmail = c.String(),
                        OrderAppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.OrderAppUser_Id)
                .Index(t => t.OrderAppUser_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        MovieID = c.Int(),
                        Quantity = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketSeat = c.String(),
                        TotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Showing_ShowingID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Showings", t => t.Showing_ShowingID)
                .ForeignKey("dbo.Movies", t => t.MovieID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.MovieID)
                .Index(t => t.Showing_ShowingID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieNumber = c.Int(nullable: false),
                        MovieTitle = c.String(),
                        Genre = c.String(),
                        MovieOverview = c.String(),
                        RunningTime = c.Int(nullable: false),
                        Tagline = c.String(),
                        MPAAratings = c.Int(nullable: false),
                        Actor = c.String(),
                        MovieRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleaseDate = c.DateTime(nullable: false),
                        FeaturedMovie = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        RatingScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Comment = c.String(maxLength: 100),
                        StarRating = c.Int(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        MovieReview_MovieID = c.Int(),
                        rating_RatingID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Movies", t => t.MovieReview_MovieID)
                .ForeignKey("dbo.Ratings", t => t.rating_RatingID)
                .Index(t => t.AppUser_Id)
                .Index(t => t.MovieReview_MovieID)
                .Index(t => t.rating_RatingID);
            
            CreateTable(
                "dbo.Showings",
                c => new
                    {
                        ShowingID = c.Int(nullable: false, identity: true),
                        ShowingNumber = c.Int(nullable: false),
                        ShowingName = c.String(),
                        ShowDate = c.DateTime(nullable: false),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Theatre = c.Int(nullable: false),
                        SponsoringMovie_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ShowingID)
                .ForeignKey("dbo.Movies", t => t.SponsoringMovie_MovieID)
                .Index(t => t.SponsoringMovie_MovieID);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Movie_MovieID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Movie_MovieID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Tickets", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Tickets", "Showing_ShowingID", "dbo.Showings");
            DropForeignKey("dbo.Showings", "SponsoringMovie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "rating_RatingID", "dbo.Ratings");
            DropForeignKey("dbo.Reviews", "MovieReview_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RatingMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.RatingMovies", "Rating_RatingID", "dbo.Ratings");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Orders", "OrderAppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cards", "AppUserId", "dbo.AspNetUsers");
            DropIndex("dbo.RatingMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.RatingMovies", new[] { "Rating_RatingID" });
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_GenreID" });
            DropIndex("dbo.Showings", new[] { "SponsoringMovie_MovieID" });
            DropIndex("dbo.Reviews", new[] { "rating_RatingID" });
            DropIndex("dbo.Reviews", new[] { "MovieReview_MovieID" });
            DropIndex("dbo.Reviews", new[] { "AppUser_Id" });
            DropIndex("dbo.Tickets", new[] { "Showing_ShowingID" });
            DropIndex("dbo.Tickets", new[] { "MovieID" });
            DropIndex("dbo.Tickets", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "OrderAppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Cards", new[] { "AppUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.RatingMovies");
            DropTable("dbo.GenreMovies");
            DropTable("dbo.Showings");
            DropTable("dbo.Reviews");
            DropTable("dbo.Ratings");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.Tickets");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Cards");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
