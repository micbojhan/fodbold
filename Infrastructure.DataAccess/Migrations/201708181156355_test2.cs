namespace Infrastructure.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "Team_Id", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "Team_Id" });
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
            
            DropColumn("dbo.Matches", "Team_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "Team_Id", c => c.Int());
            DropForeignKey("dbo.TeamMatches", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.TeamMatches", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamMatches", new[] { "Match_Id" });
            DropIndex("dbo.TeamMatches", new[] { "Team_Id" });
            DropTable("dbo.TeamMatches");
            CreateIndex("dbo.Matches", "Team_Id");
            AddForeignKey("dbo.Matches", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
