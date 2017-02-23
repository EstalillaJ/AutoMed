namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Filter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Default_Name = c.String(),
                        MAX_Money = c.String(),
                        MIN_Money = c.String(),
                        MAX_Percentage = c.String(),
                        Min_Percentage = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        household = c.String(),
                        Address = c.String(),
                        Zipe_Code = c.String(),
                        State = c.Int(nullable: false),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Filters");
        }
    }
}
