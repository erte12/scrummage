namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembersTeamsManyToManyRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberTeam",
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
            DropForeignKey("dbo.MemberTeam", "MemberId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberTeam", "TeamId", "dbo.Teams");
            DropIndex("dbo.MemberTeam", new[] { "MemberId" });
            DropIndex("dbo.MemberTeam", new[] { "TeamId" });
            DropTable("dbo.MemberTeam");
        }
    }
}
