namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeecrud : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Quotes", name: "ReviewedBy_Id", newName: "ReviewedBy_Id");
            RenameIndex(table: "dbo.Quotes", name: "IX_ReviewedBy_Id", newName: "IX_ReviewedBy_Id");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "DateReview", c => c.DateTime());
            DropColumn("dbo.Quotes", "DateReviewed");
            RenameIndex(table: "dbo.Quotes", name: "IX_ReviewedBy_Id", newName: "IX_ReviewdBy_Id");
            RenameColumn(table: "dbo.Quotes", name: "ReviewedBy_Id", newName: "ReviewdBy_Id");
        }
    }
}
