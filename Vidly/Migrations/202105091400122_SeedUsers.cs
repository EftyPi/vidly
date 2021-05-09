namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'30e12b3d-9392-437b-8fcd-bf57e7382ea2', N'admin@vidly.com', 0, N'AFGPTQFvS+r6qU278DsuGcqJeBri7S+CsO6fPltHKBX/PKsemfnxmHPwluJWJkqeqg==', N'14a6262f-fe4e-425b-9be0-610d0be740e2', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'a9977a62-6319-4d22-a681-1c55f3426ea3', N'user@vidly.com', 0, N'AMbSKq+gD6ih9BvYArwOYDv7dBaDi36OU45GxSae3OwpJkihtTUWZIp+dmgHEFS+4Q==', N'b4844831-d280-4176-9e26-e92d8a6fcca7', NULL, 0, 0, NULL, 1, 0, N'user@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e374d6a6-94c5-4ef9-a609-7eca4399e47a', N'CanManageMovies')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'30e12b3d-9392-437b-8fcd-bf57e7382ea2', N'e374d6a6-94c5-4ef9-a609-7eca4399e47a')
            ");
        }

        public override void Down()
        {
        }
    }
}
