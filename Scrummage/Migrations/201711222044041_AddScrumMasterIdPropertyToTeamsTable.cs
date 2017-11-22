namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScrumMasterIdPropertyToTeamsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "ScrumMasterId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Teams", "ScrumMasterId");
            Sql("UPDATE Teams SET ScrumMasterId='17b624d7-d793-4d17-ba74-095032d274d3'");
            AddForeignKey("dbo.Teams", "ScrumMasterId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "ScrumMasterId", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "ScrumMasterId" });
            DropColumn("dbo.Teams", "ScrumMasterId");
        }
    }
}
