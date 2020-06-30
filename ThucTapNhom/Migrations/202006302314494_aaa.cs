namespace ThucTapNhom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CustomerName", c => c.String());
            AlterColumn("dbo.Orders", "CustomerPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CustomerPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false));
        }
    }
}
