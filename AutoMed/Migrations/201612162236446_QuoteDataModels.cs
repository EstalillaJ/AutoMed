namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuoteDataModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoMedPrincipals", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.AutoMedUsers", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Quotes", "CreatedById", "dbo.AutoMedPrincipals");
            DropForeignKey("dbo.Quotes", "ApprovedById", "dbo.AutoMedPrincipals");
            DropForeignKey("dbo.Quotes", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Quotes", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.AutoMedUsers", new[] { "LocationId" });
            DropForeignKey("dbo.Documents", "Quote_Id", "dbo.Quotes");
            DropIndex("dbo.Quotes", new[] { "CreatedById" });
            DropIndex("dbo.Quotes", new[] { "LocationId" });
            DropIndex("dbo.Quotes", new[] { "CustomerId" });
            DropIndex("dbo.Quotes", new[] { "VehicleId" });
            DropIndex("dbo.AutoMedPrincipals", new[] { "Location_Id" });
            RenameColumn(table: "dbo.AutoMedUsers", name: "LocationId", newName: "Location_Id");
            RenameColumn(table: "dbo.Quotes", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Quotes", name: "ApprovedById", newName: "ApprovedBy_Id");
            RenameColumn(table: "dbo.Quotes", name: "CreatedById", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.Quotes", name: "LocationId", newName: "Location_Id");
            RenameColumn(table: "dbo.Quotes", name: "VehicleId", newName: "Vehicle_Id");
            RenameIndex(table: "dbo.Quotes", name: "IX_ApprovedById", newName: "IX_ApprovedBy_Id");
            CreateTable(
                "dbo.AutoMedUserDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LocationId = c.Int(nullable: false),
                        Role = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.QuoteDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        DateApproved = c.DateTime(nullable: false),
                        ApprovedById = c.Int(),
                        CreatedById = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        WorkDescription = c.String(),
                        LocationId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoMedUserDataModels", t => t.ApprovedById)
                .ForeignKey("dbo.AutoMedUserDataModels", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.ApprovedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LocationId)
                .Index(t => t.CustomerId)
                .Index(t => t.VehicleId);
            
            AddColumn("dbo.Documents", "QuoteDataModel_Id", c => c.Int());
            AlterColumn("dbo.AutoMedUsers", "Location_Id", c => c.Int());
            AlterColumn("dbo.AutoMedUsers", "Role", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "CreatedBy_Id", c => c.Int());
            AlterColumn("dbo.Quotes", "Location_Id", c => c.Int());
            AlterColumn("dbo.Quotes", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Quotes", "Vehicle_Id", c => c.Int());
            CreateIndex("dbo.Quotes", "CreatedBy_Id");
            CreateIndex("dbo.Quotes", "Customer_Id");
            CreateIndex("dbo.Quotes", "Location_Id");
            CreateIndex("dbo.Quotes", "Vehicle_Id");
            CreateIndex("dbo.AutoMedUsers", "Location_Id");
            CreateIndex("dbo.Documents", "QuoteDataModel_Id");
            AddForeignKey("dbo.Documents", "QuoteDataModel_Id", "dbo.QuoteDataModels", "Id");
            AddForeignKey("dbo.AutoMedUsers", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Quotes", "CreatedBy_Id", "dbo.AutoMedUsers", "Id");
            AddForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles", "Id");
            DropColumn("dbo.AutoMedUsers", "Password");
            DropColumn("dbo.Quotes", "IsApproved");
            DropTable("dbo.Quotes");
            DropTable("dbo.AutoMedPrincipals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AutoMedPrincipals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.Int(nullable: false),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Quotes", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.AutoMedUsers", "Password", c => c.String());
            DropForeignKey("dbo.Quotes", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Quotes", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Quotes", "CreatedBy_Id", "dbo.AutoMedUsers");
            DropForeignKey("dbo.Quotes", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.AutoMedUsers", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.QuoteDataModels", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.QuoteDataModels", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Documents", "QuoteDataModel_Id", "dbo.QuoteDataModels");
            DropForeignKey("dbo.QuoteDataModels", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.QuoteDataModels", "CreatedById", "dbo.AutoMedUserDataModels");
            DropForeignKey("dbo.QuoteDataModels", "ApprovedById", "dbo.AutoMedUserDataModels");
            DropForeignKey("dbo.AutoMedUserDataModels", "LocationId", "dbo.Locations");
            DropIndex("dbo.QuoteDataModels", new[] { "VehicleId" });
            DropIndex("dbo.QuoteDataModels", new[] { "CustomerId" });
            DropIndex("dbo.QuoteDataModels", new[] { "LocationId" });
            DropIndex("dbo.QuoteDataModels", new[] { "CreatedById" });
            DropIndex("dbo.QuoteDataModels", new[] { "ApprovedById" });
            DropIndex("dbo.Documents", new[] { "QuoteDataModel_Id" });
            DropIndex("dbo.AutoMedUsers", new[] { "Location_Id" });
            DropIndex("dbo.Quotes", new[] { "Vehicle_Id" });
            DropIndex("dbo.Quotes", new[] { "Location_Id" });
            DropIndex("dbo.Quotes", new[] { "Customer_Id" });
            DropIndex("dbo.Quotes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.AutoMedUserDataModels", new[] { "LocationId" });
            AlterColumn("dbo.Quotes", "Vehicle_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "Location_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "CreatedBy_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AutoMedUsers", "Role", c => c.String());
            AlterColumn("dbo.AutoMedUsers", "Location_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Documents", "QuoteDataModel_Id");
            DropTable("dbo.QuoteDataModels");
            DropTable("dbo.AutoMedUserDataModels");
            RenameIndex(table: "dbo.Quotes", name: "IX_ApprovedBy_Id", newName: "IX_ApprovedById");
            RenameColumn(table: "dbo.Quotes", name: "Vehicle_Id", newName: "VehicleId");
            RenameColumn(table: "dbo.Quotes", name: "Location_Id", newName: "LocationId");
            RenameColumn(table: "dbo.Quotes", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Quotes", name: "ApprovedBy_Id", newName: "ApprovedById");
            RenameColumn(table: "dbo.Quotes", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.AutoMedUsers", name: "Location_Id", newName: "LocationId");
            CreateIndex("dbo.AutoMedPrincipals", "Location_Id");
            CreateIndex("dbo.Quotes", "VehicleId");
            CreateIndex("dbo.Quotes", "CustomerId");
            CreateIndex("dbo.Quotes", "LocationId");
            CreateIndex("dbo.Quotes", "CreatedById");
            CreateIndex("dbo.AutoMedUsers", "LocationId");
            AddForeignKey("dbo.Quotes", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "CreatedById", "dbo.AutoMedPrincipals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quotes", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AutoMedUsers", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AutoMedPrincipals", "Location_Id", "dbo.Locations", "Id");
        }
    }
}
