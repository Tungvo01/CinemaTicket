namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Taolienket1nvoiCinemaShowdayShowTimeMovievsShow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        CinemaId = c.Int(nullable: false, identity: true),
                        CinemaName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CinemaId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.ShowDays",
                c => new
                    {
                        ShowDayId = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ShowDayId);
            
            CreateTable(
                "dbo.ShowTimes",
                c => new
                    {
                        ShowTimeId = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.ShowTimeId);
            
            AddColumn("dbo.Shows", "CinemaId", c => c.Int(nullable: false));
            AddColumn("dbo.Shows", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Shows", "ShowDayId", c => c.Int(nullable: false));
            AddColumn("dbo.Shows", "ShowTimeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Shows", "CinemaId");
            CreateIndex("dbo.Shows", "MovieId");
            CreateIndex("dbo.Shows", "ShowDayId");
            CreateIndex("dbo.Shows", "ShowTimeId");
            AddForeignKey("dbo.Shows", "CinemaId", "dbo.Cinemas", "CinemaId", cascadeDelete: true);
            AddForeignKey("dbo.Shows", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.Shows", "ShowDayId", "dbo.ShowDays", "ShowDayId", cascadeDelete: true);
            AddForeignKey("dbo.Shows", "ShowTimeId", "dbo.ShowTimes", "ShowTimeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shows", "ShowTimeId", "dbo.ShowTimes");
            DropForeignKey("dbo.Shows", "ShowDayId", "dbo.ShowDays");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Shows", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.Shows", new[] { "ShowTimeId" });
            DropIndex("dbo.Shows", new[] { "ShowDayId" });
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Shows", new[] { "CinemaId" });
            DropColumn("dbo.Shows", "ShowTimeId");
            DropColumn("dbo.Shows", "ShowDayId");
            DropColumn("dbo.Shows", "MovieId");
            DropColumn("dbo.Shows", "CinemaId");
            DropTable("dbo.ShowTimes");
            DropTable("dbo.ShowDays");
            DropTable("dbo.Movies");
            DropTable("dbo.Cinemas");
        }
    }
}
