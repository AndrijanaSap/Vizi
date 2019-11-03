namespace Vizi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Picture", c => c.Binary());
            AlterColumn("dbo.Restaurants", "SidePicture1", c => c.Binary());
            AlterColumn("dbo.Restaurants", "SidePicture2", c => c.Binary());
            AlterColumn("dbo.Restaurants", "SidePicture3", c => c.Binary());
            AlterColumn("dbo.Restaurants", "SidePicture4", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "SidePicture4", c => c.Binary(nullable: false));
            AlterColumn("dbo.Restaurants", "SidePicture3", c => c.Binary(nullable: false));
            AlterColumn("dbo.Restaurants", "SidePicture2", c => c.Binary(nullable: false));
            AlterColumn("dbo.Restaurants", "SidePicture1", c => c.Binary(nullable: false));
            AlterColumn("dbo.Restaurants", "Picture", c => c.Binary(nullable: false));
        }
    }
}
