namespace Scrummage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Surname]) VALUES (N'17b624d7-d793-4d17-ba74-095032d274d3', N'scrummaster@example.org', 0, N'AKO2V9wXi3Vdy12GhYfEMSXEQSkBTRJo4T1riCQ9X7nUdckGKxndWdztHQA1/S0OCw==', N'e4b1ac6e-b882-4b8e-9ba4-a1091cf7229f', NULL, 0, 0, NULL, 1, 0, N'scrummaster@example.org', N'Scrum', N'Master')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Surname]) VALUES (N'fcb78d65-d172-4075-881c-dc97a91a8706', N'developer@example.org', 0, N'AC+dZFCzcOog4ZUHAlTP//zN7kpIENc8CaxoBcMnze3I1LCcPC4DHYiUYLWQFGbcXw==', N'9d1625bf-2586-408f-9f88-5f48e285f75b', NULL, 0, 0, NULL, 1, 0, N'developer@example.org', N'Developer', N'Tester')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'390be627-4be3-44eb-b1af-6ac81e516540', N'ScrumMaster')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'17b624d7-d793-4d17-ba74-095032d274d3', N'390be627-4be3-44eb-b1af-6ac81e516540')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
