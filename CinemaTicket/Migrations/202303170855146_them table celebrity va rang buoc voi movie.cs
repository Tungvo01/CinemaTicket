namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themtablecelebrityvarangbuocvoimovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Celebrities",
                c => new
                    {
                        CelebrityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Height = c.String(),
                        Weight = c.String(),
                        UrlAvatar = c.String(),
                        Description = c.String(),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.CelebrityId);
            
            CreateTable(
                "dbo.MovieDetails",
                c => new
                    {
                        MovieDetailId = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        CelebrityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieDetailId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Celebrities", t => t.CelebrityId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CelebrityId);
            
            AddColumn("dbo.Movies", "Description", c => c.String());
            AddColumn("dbo.Movies", "ImageURL", c => c.String());
            AddColumn("dbo.Movies", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "Vote", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieDetails", "CelebrityId", "dbo.Celebrities");
            DropForeignKey("dbo.MovieDetails", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieDetails", new[] { "CelebrityId" });
            DropIndex("dbo.MovieDetails", new[] { "MovieId" });
            DropColumn("dbo.Movies", "Vote");
            DropColumn("dbo.Movies", "EndDate");
            DropColumn("dbo.Movies", "StartDate");
            DropColumn("dbo.Movies", "ImageURL");
            DropColumn("dbo.Movies", "Description");
            DropTable("dbo.MovieDetails");
            DropTable("dbo.Celebrities");
        }
    }
}
