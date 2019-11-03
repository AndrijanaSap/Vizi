namespace Vizi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timestamp_review : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Timestamp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Timestamp");
        }
    }
}
