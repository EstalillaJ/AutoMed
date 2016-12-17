namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyUserToLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoMedUsers", "Location_Id", "dbo.Locations");
            DropIndex("dbo.AutoMedUsers", new[] { "Location_Id" });
            RenameColumn(table: "dbo.AutoMedUsers", name: "Location_Id", newName: "LocationId");
            AlterColumn("dbo.AutoMedUsers", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.AutoMedUsers", "LocationId");
            AddForeignKey("dbo.AutoMedUsers", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AutoMedUsers", "LocationId", "dbo.Locations");
            DropIndex("dbo.AutoMedUsers", new[] { "LocationId" });
            AlterColumn("dbo.AutoMedUsers", "LocationId", c => c.Int());
            RenameColumn(table: "dbo.AutoMedUsers", name: "LocationId", newName: "Location_Id");
            CreateIndex("dbo.AutoMedUsers", "Location_Id");
            AddForeignKey("dbo.AutoMedUsers", "Location_Id", "dbo.Locations", "Id");
        }
    }
}
