namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyEmail12_26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "State", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Sex", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "CurrentNumberInHousehold", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "DiscountPercentage", c => c.Double(nullable: false));
            AddColumn("dbo.Quotes", "TotalCost", c => c.Double(nullable: false));
            DropColumn("dbo.Customers", "Gender");
            DropColumn("dbo.Quotes", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "Discount", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Quotes", "TotalCost");
            DropColumn("dbo.Quotes", "DiscountPercentage");
            DropColumn("dbo.Quotes", "CurrentNumberInHousehold");
            DropColumn("dbo.Customers", "Sex");
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "ZipCode");
            DropColumn("dbo.Customers", "State");
        }
    }
}
