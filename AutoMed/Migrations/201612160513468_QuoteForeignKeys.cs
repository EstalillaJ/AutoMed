namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuoteForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotes", "CreatedBy_Id", "dbo.AutoMedPrincipals");
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Quotes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            DropIndex("dbo.Quotes", new[] { "Location_Id" });
            DropIndex("dbo.Quotes", new[] { "Vehicle_Id" });
            RenameColumn(table: "dbo.Quotes", name: "ApprovedBy_Id", newName: "ApprovedById");
            RenameColumn(table: "dbo.Quotes", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Quotes", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.Quotes", name: "Location_Id", newName: "LocationId");
            RenameColumn(table: "dbo.Quotes", name: "Vehicle_Id", newName: "VehicleId");
            RenameIndex(table: "dbo.Quotes", name: "IX_ApprovedBy_Id", newName: "IX_ApprovedById");
            AlterColumn("dbo.Quotes", "CreatedById", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "LocationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "VehicleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "CreatedById");
            CreateIndex("dbo.Quotes", "LocationId");
            CreateIndex("dbo.Quotes", "CustomerId");
            CreateIndex("dbo.Quotes", "VehicleId");
            AddForeignKey("dbo.Quotes", "CreatedById", "dbo.AutoMedPrincipals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Quotes", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Quotes", "CreatedById", "dbo.AutoMedPrincipals");
            DropIndex("dbo.Quotes", new[] { "VehicleId" });
            DropIndex("dbo.Quotes", new[] { "CustomerId" });
            DropIndex("dbo.Quotes", new[] { "LocationId" });
            DropIndex("dbo.Quotes", new[] { "CreatedById" });
            AlterColumn("dbo.Quotes", "VehicleId", c => c.Int());
            AlterColumn("dbo.Quotes", "LocationId", c => c.Int());
            AlterColumn("dbo.Quotes", "CustomerId", c => c.Int());
            AlterColumn("dbo.Quotes", "CreatedById", c => c.Int());
            RenameIndex(table: "dbo.Quotes", name: "IX_ApprovedById", newName: "IX_ApprovedBy_Id");
            RenameColumn(table: "dbo.Quotes", name: "VehicleId", newName: "Vehicle_Id");
            RenameColumn(table: "dbo.Quotes", name: "LocationId", newName: "Location_Id");
            RenameColumn(table: "dbo.Quotes", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Quotes", name: "CreatedById", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.Quotes", name: "ApprovedById", newName: "ApprovedBy_Id");
            CreateIndex("dbo.Quotes", "Vehicle_Id");
            CreateIndex("dbo.Quotes", "Location_Id");
            CreateIndex("dbo.Quotes", "Customer_Id");
            CreateIndex("dbo.Quotes", "CreatedBy_Id");
            AddForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Quotes", "CreatedBy_Id", "dbo.AutoMedPrincipals", "Id");
        }
    }
}
