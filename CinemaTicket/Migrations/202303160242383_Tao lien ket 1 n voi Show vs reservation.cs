namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Taolienket1nvoiShowvsreservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ShowId);
            
            AddColumn("dbo.Reservations", "ShowId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "ShowId");
            AddForeignKey("dbo.Reservations", "ShowId", "dbo.Shows", "ShowId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ShowId", "dbo.Shows");
            DropIndex("dbo.Reservations", new[] { "ShowId" });
            DropColumn("dbo.Reservations", "ShowId");
            DropTable("dbo.Shows");
        }
    }
}
