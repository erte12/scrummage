namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitlePropertyToScrumTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumTasks", "Title", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrumTasks", "Title");
        }
    }
}
