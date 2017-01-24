namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class externalIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Quotes", new[] { "ReviewedBy_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Location_Id" });
            RenameColumn(table: "dbo.Quotes", name: "ReviewedBy_Id", newName: "ReviewedById");
            RenameColumn(table: "dbo.AspNetUsers", name: "Location_Id", newName: "LocationId");
            AlterColumn("dbo.Quotes", "ReviewedById", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quotes", "ReviewedById");
            CreateIndex("dbo.AspNetUsers", "LocationId");
            AddForeignKey("dbo.AspNetUsers", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
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
