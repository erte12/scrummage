namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJoinTeamRequestsTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JoinTeamRequests",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        MemberId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TeamId, t.MemberId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JoinTeamRequests", "MemberId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JoinTeamRequests", "TeamId", "dbo.Teams");
            DropIndex("dbo.JoinTeamRequests", new[] { "MemberId" });
            DropIndex("dbo.JoinTeamRequests", new[] { "TeamId" });
            DropTable("dbo.JoinTeamRequests");
        }
    }
}
