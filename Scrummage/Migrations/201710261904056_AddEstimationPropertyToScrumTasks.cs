namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEstimationPropertyToScrumTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumTasks", "EstimationId", c => c.Int());
            CreateIndex("dbo.ScrumTasks", "EstimationId");
            AddForeignKey("dbo.ScrumTasks", "EstimationId", "dbo.Estimations", "Id");
            DropColumn("dbo.ScrumTasks", "Estimation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScrumTasks", "Estimation", c => c.Byte());
            DropForeignKey("dbo.ScrumTasks", "EstimationId", "dbo.Estimations");
            DropIndex("dbo.ScrumTasks", new[] { "EstimationId" });
            DropColumn("dbo.ScrumTasks", "EstimationId");
        }
    }
}
