namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicleIds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Quotes", "Customer_Id", c => c.Int());
            AddColumn("dbo.Quotes", "Vehicle_Id", c => c.Int());
            CreateIndex("dbo.Quotes", "Customer_Id");
            CreateIndex("dbo.Quotes", "Vehicle_Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "Vehicle_Id" });
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            DropColumn("dbo.Quotes", "Vehicle_Id");
            DropColumn("dbo.Quotes", "Customer_Id");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Customers");
        }
    }
}
