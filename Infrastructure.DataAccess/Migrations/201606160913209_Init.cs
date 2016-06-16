namespace Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingNumber = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AverageGrade = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        TeamRedId = c.Int(nullable: false),
                        TeamBlueId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Done = c.Boolean(nullable: false),
                        StartTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        TimeSpan = c.Time(precision: 7),
                        ScoreDiff = c.Int(nullable: false),
                        StartGoalsTeamRed = c.Int(nullable: false),
                        EndGoalsTeamRed = c.Int(nullable: false),
                        StartGoalsTeamBlue = c.Int(nullable: false),
                        EndGoalsTeamBlue = c.Int(nullable: false),
                        GameResult = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamBlueId)
                .ForeignKey("dbo.Teams", t => t.TeamRedId)
                .Index(t => t.TeamRedId)
                .Index(t => t.TeamBlueId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        PlayerOneId = c.Int(),
                        PlayerTwoId = c.Int(),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Won = c.Int(nullable: false),
                        Draw = c.Int(nullable: false),
                        Lost = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        AllTimeHigh = c.Int(nullable: false),
                        AllTimeLow = c.Int(nullable: false),
                        GoalsAgainst = c.Int(nullable: false),
                        GoalsScored = c.Int(nullable: false),
                        GoalsAgainstHc = c.Int(nullable: false),
                        GoalsScoredHc = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerOneId)
                .ForeignKey("dbo.Players", t => t.PlayerTwoId)
                .Index(t => t.PlayerOneId)
                .Index(t => t.PlayerTwoId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        NickName = c.String(maxLength: 100),
                        FullName = c.String(maxLength: 100),
                        Initials = c.String(maxLength: 100),
                        Won = c.Int(nullable: false),
                        Draw = c.Int(nullable: false),
                        Lost = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        AllTimeHigh = c.Int(nullable: false),
                        AllTimeLow = c.Int(nullable: false),
                        GoalsAgainst = c.Int(nullable: false),
                        GoalsScored = c.Int(nullable: false),
                        GoalsAgainstHc = c.Int(nullable: false),
                        GoalsScoredHc = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 100),
                        RoleId = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 100),
                        SecurityStamp = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 100),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 100),
                        ClaimType = c.String(maxLength: 100),
                        ClaimValue = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 100),
                        ProviderKey = c.String(nullable: false, maxLength: 100),
                        UserId = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Course_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.TeamMatches",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Match_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Match_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Match_Id);
            
            CreateTable(
                "dbo.PlayerTeams",
                c => new
                    {
                        Player_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_Id, t.Team_Id })
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Player_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Matches", "TeamRedId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "TeamBlueId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "PlayerTwoId", "dbo.Players");
            DropForeignKey("dbo.Teams", "PlayerOneId", "dbo.Players");
            DropForeignKey("dbo.PlayerTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.PlayerTeams", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.TeamMatches", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.TeamMatches", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.ClassRooms", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_Id", "dbo.Students");
            DropIndex("dbo.PlayerTeams", new[] { "Team_Id" });
            DropIndex("dbo.PlayerTeams", new[] { "Player_Id" });
            DropIndex("dbo.TeamMatches", new[] { "Match_Id" });
            DropIndex("dbo.TeamMatches", new[] { "Team_Id" });
            DropIndex("dbo.StudentCourses", new[] { "Course_Id" });
            DropIndex("dbo.StudentCourses", new[] { "Student_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Teams", new[] { "PlayerTwoId" });
            DropIndex("dbo.Teams", new[] { "PlayerOneId" });
            DropIndex("dbo.Matches", new[] { "TeamBlueId" });
            DropIndex("dbo.Matches", new[] { "TeamRedId" });
            DropIndex("dbo.Courses", new[] { "Teacher_Id" });
            DropIndex("dbo.ClassRooms", new[] { "Course_Id" });
            DropTable("dbo.PlayerTeams");
            DropTable("dbo.TeamMatches");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tests");
            DropTable("dbo.Teachers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Players");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.ClassRooms");
        }
    }
}
