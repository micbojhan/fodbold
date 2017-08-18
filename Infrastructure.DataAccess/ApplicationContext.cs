using Core.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Core.DomainModel.Model;
using Core.DomainModel.Model.New;
using Core.DomainModel.OldModel;
using Infrastructure.DataAccess.Seeding;
using Match = Core.DomainModel.Model.Match;

namespace Infrastructure.DataAccess
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        private static string databaseString = "fodboldv2";
        // throwIfV1Schema is used when upgrading Identity in a database from 1 to 2.
        // It's a one time thing and can be safely removed.
        public ApplicationContext()
            : base(databaseString, throwIfV1Schema: false)
        {
            // Database.SetInitializer<ApplicationContext>(null);
            // The database initializer will create the database and the specified tables.
            // If you're using an existing database with code first, don't execute any logic at all.

            // Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationContext>()); // Default, will run if nothing is done.
            // The default option. When the application runs the first time, entity framework will create
            // a code first database if it does not already exist. If the database exists and you have done modifications
            // this will throw an InvalidOperationException.

            // Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationContext>());
            // This option, as the name suggests, will always drop and recreate the database when the application runs the first time.
            // All tables will be deleted as the database is dropped.

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
            // This option will drop and recreate the database if there are any changes to the model.

            // Alternatively, you can create your own initializer and pass it as the argument.
            // The class will need to implement one of the above in order to inherit some behaviour.
            // These Initializers has a custom seeding function, that will populate the database instance
            // with some fake data that can be used right away.
            if (Database.Exists(databaseString))
            {
                // Runs if a database already exists. It drops the database, recreates it and seeds with some fake data.
                Database.SetInitializer(new ChangeSampleSeedInitializer());
            }
            else
            {
                // This initilizer will run if there are no database.
                // Usually this is on the first run, or if the database was deleted.
                // Seeds with some fake data.
                Database.SetInitializer(new CreateSampleSeedInitializer());
            }
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        // Define you conceptual model here. Entity Framework will include these types and all their references.
        // There are 3 different inheritance strategies:
        // * Table per Hierarchy (TPH) (default): http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-1-table-per-hierarchy-tph
        // * Table per Type (TPT): http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
        // * Table per Concrete class (TPC): http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-3-table-per-concrete-type-tpc-and-choosing-strategy-guidelines

        //public IDbSet<GameResult> GameResults { get; set; }
        //public IDbSet<MatchPlayer> MatchPlayers { get; set; }
        //public IDbSet<MatchTeam> MatchTeams { get; set; }
        //public IDbSet<TeamPlayer> TeamPlayers { get; set; }

        public IDbSet<Test> Tests { get; set; }
        public IDbSet<Player> Players { get; set; }
        public IDbSet<Team> Teams { get; set; }
        public IDbSet<Match> Matches { get; set; }
        //public IDbSet<Derby> Derbys { get; set; }


        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<ClassRoom> ClassRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // The DateTime type in .NET has the same range and precision as datetime2 in SQL Server.
            // Configure DateTime type to use SQL server datetime2 instead.
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // Consider setting max length on string properties.
            // http://dba.stackexchange.com/questions/48408/ef-code-first-uses-nvarcharmax-for-all-strings-will-this-hurt-query-performan
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));

            // Set max length where it makes sense.
            modelBuilder.Entity<Student>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Teacher>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Course>().Property(s => s.Name).HasMaxLength(30);
            /////////////////////////////////////////////////////////
            //modelBuilder.Entity<Derby>().HasKey(k => k.Id);
            //modelBuilder.Entity<GameResult>().HasKey(k => k.Id);
            //modelBuilder.Entity<MatchPlayer>().HasKey(k => k.Id);
            //modelBuilder.Entity<MatchTeam>().HasKey(k => k.Id);
            modelBuilder.Entity<Match>().HasKey(k => k.Id);
            modelBuilder.Entity<Player>().HasKey(k => k.Id);
            modelBuilder.Entity<Team>().HasKey(k => k.Id);
            modelBuilder.Entity<Test>().HasKey(k => k.Id);

            ////////////////////////DERBY/////////////////////////////////
            //modelBuilder.Entity<Derby>()
            //    .HasRequired(p => p.TeamOne)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamOneId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Derby>()
            //    .HasRequired(p => p.TeamTwo)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamTwoId)
            //    .WillCascadeOnDelete(false);
            //////////////////////////TEAM///////////////////////////////
            //modelBuilder.Entity<Team>()
            //    .HasMany(t => t.Derbies)
            //    .WithMany();

            //modelBuilder.Entity<Team>()
            //    .HasMany(t => t.MatchTeam)
            //    .WithMany();

            modelBuilder.Entity<Team>()
                .HasRequired(p => p.PlayerOne)
                .WithMany()
                .HasForeignKey(p => p.PlayerOneId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(p => p.PlayerTwo)
                .WithMany()
                .HasForeignKey(p => p.PlayerTwoId)
                .WillCascadeOnDelete(false);

            //////////////////////////MatchPlayer///////////////////////////////
            //modelBuilder.Entity<MatchPlayer>()
            //    .HasMany(t => t.Matches)
            //    .WithMany();

            //modelBuilder.Entity<MatchPlayer>()
            //    .HasRequired(p => p.Player)
            //    .WithMany()
            //    .HasForeignKey(p => p.PlayerId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<MatchPlayer>()
            //    .HasRequired(p => p.Match)
            //    .WithMany()
            //    .HasForeignKey(p => p.MatchId)
            //    .WillCascadeOnDelete(false);
            //////////////////////////MATCH TEAM ///////////////////////////////
            //modelBuilder.Entity<MatchTeam>()
            //    .HasMany(t => t.Matches)
            //    .WithMany();

            //modelBuilder.Entity<MatchTeam>()
            //    .HasRequired(p => p.Team)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<MatchTeam>()
            //    .HasRequired(p => p.Match)
            //    .WithMany()
            //    .HasForeignKey(p => p.MatchId)
            //    .WillCascadeOnDelete(false);
            ////////////////////////// PLAYER ///////////////////////////////
            //modelBuilder.Entity<Player>()
            //    .HasMany(t => t.MatchPlayer)
            //    .WithMany();

            //modelBuilder.Entity<Player>()
            //    .HasMany(t => t.Teams)
            //    .WithMany();
            /////////////////////////// MATCH //////////////////////////////
            //modelBuilder.Entity<Match>()
            //    .HasMany(t => t.MatchTeam)
            //    .WithMany();

            //modelBuilder.Entity<Match>()
            //    .HasMany(t => t.MatchPlayer)
            //    .WithMany();

            modelBuilder.Entity<Match>()
                .HasRequired(p => p.TeamRed)
                .WithMany()
                .HasForeignKey(p => p.TeamRedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasRequired(p => p.TeamBlue)
                .WithMany()
                .HasForeignKey(p => p.TeamBlueId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Team>().HasMany(m => m.Matches).WithMany();
            //modelBuilder.Entity<Player>().HasMany(m => m.Teams).WithMany();
            //modelBuilder.Entity<Match>()
            //    .HasRequired(p => p.TeamRedPlayerOne)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamRedPlayerOneId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Match>()
            //    .HasRequired(p => p.TeamRedPlayerTwo)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamRedPlayerTwoId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Match>()
            //    .HasRequired(p => p.TeamBluePlayerOne)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamBluePlayerOneId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Match>()
            //    .HasRequired(p => p.TeamBluePlayerTwo)
            //    .WithMany()
            //    .HasForeignKey(p => p.TeamBluePlayerTwoId)
            //    .WillCascadeOnDelete(false);
            /////////////////////////////////////////////////////////


            // Primary keys
            //modelBuilder.Entity<Team>().HasKey(k => k.Id);
            //modelBuilder.Entity<Player>().HasKey(q => q.Id);
            //modelBuilder.Entity<Match>().HasKey(q => q.Id);

            //modelBuilder.Entity<MatchTeam>().HasKey(q => q.Id);
            //modelBuilder.Entity<MatchPlayer>().HasKey(q => q.Id);
            //modelBuilder.Entity<TeamPlayer>().HasKey(q => q.Id);
            ////modelBuilder.Entity<MatchPlayer>().HasKey(q =>
            ////    new {
            ////        q.PlayerId,
            ////        q.MatchId
            ////    });
            //// Relationships
            //modelBuilder.Entity<MatchPlayer>()
            //    .HasRequired(t => t.Player)
            //    .WithMany(t => t.MatchPlayer)
            //    .HasForeignKey(t => t.PlayerId);

            //modelBuilder.Entity<MatchPlayer>()
            //    .HasRequired(t => t.Match)
            //    .WithMany(t => t.MatchPlayer)
            //    .HasForeignKey(t => t.MatchId);

            //modelBuilder.Entity<MatchTeam>()
            //    .HasRequired(t => t.Team)
            //    .WithMany(t => t.MatchTeam)
            //    .HasForeignKey(t => t.TeamId);

            //modelBuilder.Entity<MatchTeam>()
            //    .HasRequired(t => t.Match)
            //    .WithMany(t => t.MatchTeam)
            //    .HasForeignKey(t => t.MatchId);

            //modelBuilder.Entity<TeamPlayer>()
            //    .HasRequired(t => t.Team)
            //    .WithMany(t => t.TeamPlayer)
            //    .HasForeignKey(t => t.TeamId);

            //modelBuilder.Entity<TeamPlayer>()
            //    .HasRequired(t => t.Player)
            //    .WithMany(t => t.TeamPlayer)
            //    .HasForeignKey(t => t.PlayerId);



            base.OnModelCreating(modelBuilder);

        }
    }
}
