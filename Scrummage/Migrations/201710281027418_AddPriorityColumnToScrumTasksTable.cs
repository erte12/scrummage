namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriorityColumnToScrumTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumTasks", "Priority", c => c.Byte());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrumTasks", "Priority");
        }
    }
}
