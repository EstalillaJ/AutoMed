namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quoteenum : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Quotes", "Approval");

            AddColumn("dbo.Quotes", "Approval", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "Approved", c => c.Boolean(nullable: false));
        }
    }
}
