namespace Vizi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Telephone", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Email", c => c.String());
            AlterColumn("dbo.Restaurants", "Telephone", c => c.String());
            AlterColumn("dbo.Restaurants", "Address", c => c.String());
            AlterColumn("dbo.Restaurants", "Name", c => c.String());
        }
    }
}
