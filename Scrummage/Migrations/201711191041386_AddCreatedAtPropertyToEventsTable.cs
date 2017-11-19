namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtPropertyToEventsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "CreatedAt");
        }
    }
}
