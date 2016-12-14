namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoMedUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location_Name = c.String(),
                        Role = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Comments = c.String(),
                        Quote_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotes", t => t.Quote_Id)
                .Index(t => t.Quote_Id);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        DateApproved = c.DateTime(nullable: false),
                        Discount = c.Double(nullable: false),
                        isApproved = c.Boolean(nullable: false),
                        WorkDescription = c.String(),
                        Location_Name = c.String(),
                        ApprovedBy_Id = c.Int(),
                        CreatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoMedPrincipals", t => t.ApprovedBy_Id)
                .ForeignKey("dbo.AutoMedPrincipals", t => t.CreatedBy_Id)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.AutoMedPrincipals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location_Name = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "Quote_Id", "dbo.Quotes");
            DropForeignKey("dbo.Quotes", "CreatedBy_Id", "dbo.AutoMedPrincipals");
            DropForeignKey("dbo.Quotes", "ApprovedBy_Id", "dbo.AutoMedPrincipals");
            DropIndex("dbo.Quotes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Quotes", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.Documents", new[] { "Quote_Id" });
            DropTable("dbo.AutoMedPrincipals");
            DropTable("dbo.Quotes");
            DropTable("dbo.Documents");
            DropTable("dbo.AutoMedUsers");
        }
    }
}
