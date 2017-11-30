namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeScrumTaskDescriptionMasLengthTo2000Characters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ScrumTasks", "Content", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScrumTasks", "Content", c => c.String(nullable: false, maxLength: 400));
        }
    }
}
