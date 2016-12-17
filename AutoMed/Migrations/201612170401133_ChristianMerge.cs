namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChristianMerge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Vehicles", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Vehicles", name: "Customer_Id", newName: "OwnerId");
            AddColumn("dbo.Customers", "AddressLine1", c => c.String());
            AddColumn("dbo.Customers", "AddressLine2", c => c.String());
            AddColumn("dbo.Vehicles", "LicensePlate", c => c.String());
            AlterColumn("dbo.Vehicles", "Color", c => c.Int(nullable: false));
            AlterColumn("dbo.Vehicles", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "OwnerId");
            AddForeignKey("dbo.Vehicles", "OwnerId", "dbo.Customers", "Id", cascadeDelete: false);
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Vehicles", "LicensePlateNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "LicensePlateNumber", c => c.String());
            AddColumn("dbo.Customers", "Address", c => c.String());
            DropForeignKey("dbo.Vehicles", "OwnerId", "dbo.Customers");
            DropIndex("dbo.Vehicles", new[] { "OwnerId" });
            AlterColumn("dbo.Vehicles", "OwnerId", c => c.Int());
            AlterColumn("dbo.Vehicles", "Color", c => c.String());
            DropColumn("dbo.Vehicles", "LicensePlate");
            DropColumn("dbo.Customers", "AddressLine2");
            DropColumn("dbo.Customers", "AddressLine1");
            RenameColumn(table: "dbo.Vehicles", name: "OwnerId", newName: "Customer_Id");
            CreateIndex("dbo.Vehicles", "Customer_Id");
            AddForeignKey("dbo.Vehicles", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
