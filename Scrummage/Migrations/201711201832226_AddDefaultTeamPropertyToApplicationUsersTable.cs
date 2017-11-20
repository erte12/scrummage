namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultTeamPropertyToApplicationUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DefaultTeamId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "DefaultTeamId");
            AddForeignKey("dbo.AspNetUsers", "DefaultTeamId", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DefaultTeamId", "dbo.Teams");
            DropIndex("dbo.AspNetUsers", new[] { "DefaultTeamId" });
            DropColumn("dbo.AspNetUsers", "DefaultTeamId");
        }
    }
}
