namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class externalIds : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Quotes", new[] { "ReviewedBy_Id" });
            RenameColumn(table: "dbo.Quotes", name: "ReviewedBy_Id", newName: "ReviewedById");
            AlterColumn("dbo.Quotes", "ReviewedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.Quotes", "ReviewedById");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LocationId", "dbo.Locations");
            DropIndex("dbo.AspNetUsers", new[] { "LocationId" });
            DropIndex("dbo.Quotes", new[] { "ReviewedById" });
            AlterColumn("dbo.AspNetUsers", "LocationId", c => c.Int());
            AlterColumn("dbo.Quotes", "ReviewedById", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.AspNetUsers", name: "LocationId", newName: "Location_Id");
            RenameColumn(table: "dbo.Quotes", name: "ReviewedById", newName: "ReviewedBy_Id");
            AddColumn("dbo.Quotes", "ReviewedById", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Location_Id");
            CreateIndex("dbo.Quotes", "ReviewedBy_Id");
            AddForeignKey("dbo.AspNetUsers", "Location_Id", "dbo.Locations", "Id");
        }
    }
}
