namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "AnnualIncome", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "AnnualIncome");
        }
    }
}
