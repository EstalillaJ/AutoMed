namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationUniqueName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.Locations", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Locations", new[] { "Name" });
            AlterColumn("dbo.Locations", "Name", c => c.String());
        }
    }
}
