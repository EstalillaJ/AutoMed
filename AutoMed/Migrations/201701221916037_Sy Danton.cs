namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyDanton : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "AnnualIncome", c => c.Double(nullable: false));
            AddColumn("dbo.Documents", "Title", c => c.String());
            AddColumn("dbo.Documents", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Comments");
            DropColumn("dbo.Documents", "Title");
            DropColumn("dbo.Quotes", "AnnualIncome");
        }
    }
}
