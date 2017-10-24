namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScrumTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 400),
                        Estimation = c.Byte(nullable: false),
                        Took = c.Byte(),
                        TaskType = c.Byte(nullable: false),
                        SprintId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sprints", t => t.SprintId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SprintId)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Sprints", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Sprints", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScrumTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ScrumTasks", "SprintId", "dbo.Sprints");
            DropIndex("dbo.ScrumTasks", new[] { "UserId" });
            DropIndex("dbo.ScrumTasks", new[] { "SprintId" });
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.Sprints", "Description", c => c.String());
            AlterColumn("dbo.Sprints", "Name", c => c.String());
            DropTable("dbo.ScrumTasks");
        }
    }
}
