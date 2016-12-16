namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersAndVehicles1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Color", c => c.String());
            AddColumn("dbo.Vehicles", "LicensePlateNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "LicensePlateNumber");
            DropColumn("dbo.Vehicles", "Color");
        }
    }
}
