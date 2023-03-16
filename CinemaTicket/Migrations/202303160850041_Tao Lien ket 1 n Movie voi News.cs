namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaoLienket1nMovievoiNews : DbMigration
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
                    })
                .PrimaryKey(t => t.NewsId);
            
            AddColumn("dbo.Movies", "NewsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "NewsId");
            AddForeignKey("dbo.Movies", "NewsId", "dbo.News", "NewsId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "NewsId", "dbo.News");
            DropIndex("dbo.Movies", new[] { "NewsId" });
            DropColumn("dbo.Movies", "NewsId");
            DropTable("dbo.News");
        }
    }
}
