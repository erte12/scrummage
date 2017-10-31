namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTookPropertyToEstimationinScrumTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumTasks", "TookId", c => c.Int());
            CreateIndex("dbo.ScrumTasks", "TookId");
            AddForeignKey("dbo.ScrumTasks", "TookId", "dbo.Estimations", "Id");
            DropColumn("dbo.ScrumTasks", "Took");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScrumTasks", "Took", c => c.Byte());
            DropForeignKey("dbo.ScrumTasks", "TookId", "dbo.Estimations");
            DropIndex("dbo.ScrumTasks", new[] { "TookId" });
            DropColumn("dbo.ScrumTasks", "TookId");
        }
    }
}
