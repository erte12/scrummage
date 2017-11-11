namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtColumnToScrumTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumTasks", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrumTasks", "CreatedAt");
        }
    }
}
