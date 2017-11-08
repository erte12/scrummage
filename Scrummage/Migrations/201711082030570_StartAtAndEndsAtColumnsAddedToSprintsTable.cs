namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartAtAndEndsAtColumnsAddedToSprintsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sprints", "StartsAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sprints", "EndsAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sprints", "EndsAt");
            DropColumn("dbo.Sprints", "StartsAt");
        }
    }
}
