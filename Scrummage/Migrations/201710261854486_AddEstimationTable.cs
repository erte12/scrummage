namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEstimationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estimations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Estimations");
        }
    }
}
