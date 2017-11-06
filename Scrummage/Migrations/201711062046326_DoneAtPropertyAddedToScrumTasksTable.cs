namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoneAtPropertyAddedToScrumTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumTasks", "DoneAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrumTasks", "DoneAt");
        }
    }
}
