namespace ThucTapNhom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_price_to_int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
        }
    }
}
