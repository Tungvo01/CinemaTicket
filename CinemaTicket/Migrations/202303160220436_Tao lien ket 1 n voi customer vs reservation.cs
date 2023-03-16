namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Taolienket1nvoicustomervsreservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropTable("dbo.Reservations");
        }
    }
}
