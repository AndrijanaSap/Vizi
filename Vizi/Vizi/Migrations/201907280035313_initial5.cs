namespace Vizi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Picture");
        }
    }
}
