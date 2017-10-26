namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedEstimationsTableWithData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Estimations (Value) VALUES (0)");
            Sql("INSERT INTO Estimations (Value) VALUES (1)");
            Sql("INSERT INTO Estimations (Value) VALUES (2)");
            Sql("INSERT INTO Estimations (Value) VALUES (3)");
            Sql("INSERT INTO Estimations (Value) VALUES (5)");
            Sql("INSERT INTO Estimations (Value) VALUES (8)");
            Sql("INSERT INTO Estimations (Value) VALUES (13)");
            Sql("INSERT INTO Estimations (Value) VALUES (20)");
            Sql("INSERT INTO Estimations (Value) VALUES (40)");
        }
        
        public override void Down()
        {
        }
    }
}
