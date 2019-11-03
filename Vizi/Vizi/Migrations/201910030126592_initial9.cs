namespace Vizi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "SidePicture1", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "SidePicture1");
        }
    }
}
