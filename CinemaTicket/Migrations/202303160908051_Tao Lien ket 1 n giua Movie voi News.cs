namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaoLienket1ngiuaMovievoiNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "MovieId", "dbo.Movies");
            DropIndex("dbo.News", new[] { "MovieId" });
            DropTable("dbo.News");
        }
    }
}
