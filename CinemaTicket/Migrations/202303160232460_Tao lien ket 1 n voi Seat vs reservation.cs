namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Taolienket1nvoiSeatvsreservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        SeatNo = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SeatId);
            
            AddColumn("dbo.Reservations", "SeatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "SeatId");
            AddForeignKey("dbo.Reservations", "SeatId", "dbo.Seats", "SeatId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "SeatId", "dbo.Seats");
            DropIndex("dbo.Reservations", new[] { "SeatId" });
            DropColumn("dbo.Reservations", "SeatId");
            DropTable("dbo.Seats");
        }
    }
}
