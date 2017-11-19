namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEndsAtPropertyInEventsTableToRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "EndsAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EndsAt", c => c.DateTime());
        }
    }
}
