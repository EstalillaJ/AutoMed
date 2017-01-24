namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixspelling : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Quotes", name: "ReviewdBy_Id", newName: "ReviewedBy_Id");
            RenameIndex(table: "dbo.Quotes", name: "IX_ReviewdBy_Id", newName: "IX_ReviewedBy_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Quotes", name: "IX_ReviewedBy_Id", newName: "IX_ReviewdBy_Id");
            RenameColumn(table: "dbo.Quotes", name: "ReviewedBy_Id", newName: "ReviewdBy_Id");
        }
    }
}
