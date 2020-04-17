namespace CinemaApp.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieHalls",
                c => new
                    {
                        MovieHallID = c.Int(nullable: false, identity: true),
                        MovieHallNo = c.String(),
                        Rows = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieHallID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MoviesID = c.Int(nullable: false, identity: true),
                        MovieTitle = c.String(),
                        MovieReleaseTime = c.DateTime(nullable: false),
                        MovieAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MoviesID);
            
            CreateTable(
                "dbo.MovieTimes",
                c => new
                    {
                        MovieTimesID = c.Int(nullable: false, identity: true),
                        MovieTimeStart = c.DateTime(nullable: false),
                        MovieHallNo = c.String(),
                        MoviesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieTimesID)
                .ForeignKey("dbo.Movies", t => t.MoviesID, cascadeDelete: true)
                .Index(t => t.MoviesID);
            
            CreateTable(
                "dbo.MovieSeats",
                c => new
                    {
                        MovieSeatsID = c.Int(nullable: false, identity: true),
                        SeatNo = c.String(),
                        SeatAvail = c.Int(nullable: false),
                        UsersID = c.Int(),
                        MovieTimesID = c.Int(),
                    })
                .PrimaryKey(t => t.MovieSeatsID)
                .ForeignKey("dbo.MovieTimes", t => t.MovieTimesID)
                .ForeignKey("dbo.Users", t => t.UsersID)
                .Index(t => t.UsersID)
                .Index(t => t.MovieTimesID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsersID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UsersID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieSeats", "UsersID", "dbo.Users");
            DropForeignKey("dbo.MovieSeats", "MovieTimesID", "dbo.MovieTimes");
            DropForeignKey("dbo.MovieTimes", "MoviesID", "dbo.Movies");
            DropIndex("dbo.MovieSeats", new[] { "MovieTimesID" });
            DropIndex("dbo.MovieSeats", new[] { "UsersID" });
            DropIndex("dbo.MovieTimes", new[] { "MoviesID" });
            DropTable("dbo.Users");
            DropTable("dbo.MovieSeats");
            DropTable("dbo.MovieTimes");
            DropTable("dbo.Movies");
            DropTable("dbo.MovieHalls");
        }
    }
}
