namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersAndVehicles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            AddColumn("dbo.Customers", "LastName", c => c.String());
            AddColumn("dbo.Customers", "Address", c => c.String());
            AddColumn("dbo.Customers", "Email", c => c.String());
            AddColumn("dbo.Customers", "PhoneNumber", c => c.String());
            AddColumn("dbo.Customers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "Vin", c => c.String());
            AddColumn("dbo.Vehicles", "Make", c => c.String());
            AddColumn("dbo.Vehicles", "Model", c => c.String());
            AddColumn("dbo.Vehicles", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Vehicles", "Customer_Id");
            AddForeignKey("dbo.Vehicles", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Vehicles", new[] { "Customer_Id" });
            DropColumn("dbo.Vehicles", "Customer_Id");
            DropColumn("dbo.Vehicles", "Year");
            DropColumn("dbo.Vehicles", "Model");
            DropColumn("dbo.Vehicles", "Make");
            DropColumn("dbo.Vehicles", "Vin");
            DropColumn("dbo.Customers", "Gender");
            DropColumn("dbo.Customers", "Age");
            DropColumn("dbo.Customers", "PhoneNumber");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
