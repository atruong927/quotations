namespace QuotationsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotations", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Quotations", new[] { "CategoryID" });
            AlterColumn("dbo.Quotations", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Quotations", "Quote", c => c.String(nullable: false));
            AlterColumn("dbo.Quotations", "CategoryID", c => c.Int());
            CreateIndex("dbo.Quotations", "CategoryID");
            AddForeignKey("dbo.Quotations", "CategoryID", "dbo.Categories", "CategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Quotations", new[] { "CategoryID" });
            AlterColumn("dbo.Quotations", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotations", "Quote", c => c.String());
            AlterColumn("dbo.Quotations", "Author", c => c.String());
            CreateIndex("dbo.Quotations", "CategoryID");
            AddForeignKey("dbo.Quotations", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
    }
}
