namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class externalIdsToQuotes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Quotes", name: "Customer_Id", newName: "CustomerId");
            AlterColumn("dbo.Quotes", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "CustomerId");
            AddForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);

            DropForeignKey("dbo.Quotes", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Quotes", new[] { "CreatedBy_Id" });
            RenameColumn(table: "dbo.Quotes", name: "CreatedBy_Id", newName: "CreatedById");
            AlterColumn("dbo.Quotes", "ReviewedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.Quotes", "CreatedById");
            AddForeignKey("dbo.Quotes", "CreatedById", "dbo.AspNetUsers", "Id", cascadeDelete: true);

            DropForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Quotes", new[] { "Location_Id" });
            RenameColumn(table: "dbo.Quotes", name: "Location_Id", newName: "LocationId");
            AlterColumn("dbo.Quotes", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "LocationId");
            AddForeignKey("dbo.Quotes", "LocationId", "dbo.Locations", "Id", cascadeDelete: false);

            DropForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Quotes", new[] { "Vehicle_Id" });
            RenameColumn(table: "dbo.Quotes", name: "Vehicle_Id", newName: "VehicleId");
            AlterColumn("dbo.Quotes", "VehicleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "VehicleId");
            AddForeignKey("dbo.Quotes", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "CustomerId" });
            AlterColumn("dbo.Quotes", "CustomerId", c => c.Int());
            RenameColumn(table: "dbo.Quotes", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.Quotes", "Customer_Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id");

            DropForeignKey("dbo.Quotes", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Quotes", new[] { "VehicleId" });
            AlterColumn("dbo.Quotes", "VehicleId", c => c.Int());
            RenameColumn(table: "dbo.Quotes", name: "VehicleId", newName: "Vehicle_Id");
            CreateIndex("dbo.Quotes", "Vehicle_Id");
            AddForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles", "Id");

            DropForeignKey("dbo.Quotes", "LocationId", "dbo.Locations");
            DropIndex("dbo.Quotes", new[] { "LocationId" });
            AlterColumn("dbo.Quotes", "LocationId", c => c.Int());
            RenameColumn(table: "dbo.Quotes", name: "LocationId", newName: "Location_Id");
            CreateIndex("dbo.Quotes", "Location_Id");
            AddForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations", "Id");

            DropForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Quotes", new[] { "CustomerId" });
            AlterColumn("dbo.Quotes", "CustomerId", c => c.Int());
            RenameColumn(table: "dbo.Quotes", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.Quotes", "Customer_Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
