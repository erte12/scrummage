namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 200),
                        TeamId = c.Int(nullable: false),
                        StartsAt = c.DateTime(nullable: false),
                        EndsAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "TeamId", "dbo.Teams");
            DropIndex("dbo.Events", new[] { "TeamId" });
            DropTable("dbo.Events");
        }
    }
}
