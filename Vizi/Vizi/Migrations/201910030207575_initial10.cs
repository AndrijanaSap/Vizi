namespace Vizi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "SidePicture2", c => c.Binary());
            AddColumn("dbo.Restaurants", "SidePicture3", c => c.Binary());
            AddColumn("dbo.Restaurants", "SidePicture4", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "SidePicture4");
            DropColumn("dbo.Restaurants", "SidePicture3");
            DropColumn("dbo.Restaurants", "SidePicture2");
        }
    }
}
