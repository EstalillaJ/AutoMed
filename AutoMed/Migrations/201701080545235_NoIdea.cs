namespace AutoMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoIdea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "DateReviewed", c => c.DateTime());
            DropColumn("dbo.Quotes", "DateReview");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "DateReview", c => c.DateTime());
            DropColumn("dbo.Quotes", "DateReviewed");
        }
    }
}
