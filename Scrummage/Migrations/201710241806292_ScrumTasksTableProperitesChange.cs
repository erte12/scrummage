namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScrumTasksTableProperitesChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScrumTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ScrumTasks", new[] { "UserId" });
            AlterColumn("dbo.ScrumTasks", "Estimation", c => c.Byte());
            AlterColumn("dbo.ScrumTasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ScrumTasks", "UserId");
            AddForeignKey("dbo.ScrumTasks", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScrumTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ScrumTasks", new[] { "UserId" });
            AlterColumn("dbo.ScrumTasks", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ScrumTasks", "Estimation", c => c.Byte(nullable: false));
            CreateIndex("dbo.ScrumTasks", "UserId");
            AddForeignKey("dbo.ScrumTasks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
