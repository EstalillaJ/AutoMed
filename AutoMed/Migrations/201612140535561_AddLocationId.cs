namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AutoMedUsers", "Location_Id", c => c.Int());
            AddColumn("dbo.Quotes", "Location_Id", c => c.Int());
            AddColumn("dbo.AutoMedPrincipals", "Location_Id", c => c.Int());
            CreateIndex("dbo.AutoMedUsers", "Location_Id");
            CreateIndex("dbo.Quotes", "Location_Id");
            CreateIndex("dbo.AutoMedPrincipals", "Location_Id");
            AddForeignKey("dbo.AutoMedUsers", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.AutoMedPrincipals", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations", "Id");
            DropColumn("dbo.AutoMedUsers", "Location_Name");
            DropColumn("dbo.Quotes", "Location_Name");
            DropColumn("dbo.AutoMedPrincipals", "Location_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoMedPrincipals", "Location_Name", c => c.String());
            AddColumn("dbo.Quotes", "Location_Name", c => c.String());
            AddColumn("dbo.AutoMedUsers", "Location_Name", c => c.String());
            DropForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.AutoMedPrincipals", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.AutoMedUsers", "Location_Id", "dbo.Locations");
            DropIndex("dbo.AutoMedPrincipals", new[] { "Location_Id" });
            DropIndex("dbo.Quotes", new[] { "Location_Id" });
            DropIndex("dbo.AutoMedUsers", new[] { "Location_Id" });
            DropColumn("dbo.AutoMedPrincipals", "Location_Id");
            DropColumn("dbo.Quotes", "Location_Id");
            DropColumn("dbo.AutoMedUsers", "Location_Id");
            DropTable("dbo.Locations");
        }
    }
}
