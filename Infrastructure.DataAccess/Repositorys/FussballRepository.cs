using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.DomainModel.Model;
using Core.DomainServices;

namespace Infrastructure.DataAccess.Repositorys
{
    public class FussballRepository : IFussballRepository
    {
        private readonly ApplicationContext _dbContext;
        //private readonly DbSet<Player> _dbSet;

        public FussballRepository(ApplicationContext context)
        {
            _dbContext = context;
            //_dbSet = context.Set<Player>();
        }

        public List<Player> GetPlayerList()
        {
            var players = _dbContext.Players
                .Include(p => p.Teams.Select(c => c.Matches))
                .OrderBy(p => p.Score)
                .ThenBy(p => p.AllTimeHigh)
                .ThenBy(p => p.Name)
                .ToList();
            return players;
        }

        public List<Team> GetTeamList()
        {
            var teams = _dbContext.Teams
                .Include(p => p.Matches)
                .Include(p => p.PlayerOne)
                .Include(p => p.PlayerTwo)
                .OrderBy(p => p.Score)
                .ThenBy(p => p.AllTimeHigh)
                .ToList();
            return teams;
        }

        public List<Match> GetMatchList()
        {
            var matches = _dbContext.Matches
                .OrderByDescending(m => m.StartTime)
                .ToList();
            return matches;
        }

        public Team GetTeam(int teamId)
        {
            var team = _dbContext.Teams
                .Include(p => p.Matches)
                .Include(p => p.PlayerOne)
                .Include(p => p.PlayerTwo)
                .FirstOrDefault(p => p.Id == teamId);
            return team;
        }

        public Player GetPlayer(int playerId)
        {
            var player = _dbContext.Players
                .Include(p => p.Teams.Select(t => t.Matches))
                .FirstOrDefault(p => p.Id == playerId);
            return player;
        }

        public Player CreatePlayer(Player p)
        {
            var player = _dbContext.Players.Add(p);
            return player;
        }

        public Team CreateOrGetTeam(int playerOneId, int playerTwoId)
        {
            var team = GetTeam(playerOneId, playerTwoId);
            if (team != null) return team;
            team = new Team();
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();

            var playerOne = GetPlayer(playerOneId);
            var playerTwo = GetPlayer(playerTwoId);
            team = new Team
            {
                PlayerOne = playerOne,
                PlayerOneId = playerOne.Id,
                PlayerTwo = playerTwo,
                PlayerTwoId = playerTwo.Id,
            };
            _dbContext.Teams.Add(team);

            return team;
        }

        public Team GetTeam(int playerOneId, int playerTwoId)
        {
            var team = _dbContext.Teams
                 .Include(t => t.PlayerOne)
                 .Include(t => t.PlayerTwo)
                 .FirstOrDefault(t => (t.PlayerOneId == playerOneId && t.PlayerTwoId == playerTwoId) ||
                                      (t.PlayerTwoId == playerOneId && t.PlayerOneId == playerTwoId));

            return team;
        }


        public Match CreateMatch(int playerOneId, int playerTwoId, int playerThreeId, int playerFourId)
        {
            var teamRed = GetTeam(playerOneId, playerTwoId);
            var teamBlue = GetTeam(playerThreeId, playerFourId);

            var match = new Match
            {
                StartTime = DateTime.UtcNow,
                EndTime = null,
                TimeSpan = null,
                TeamRedId = teamRed.Id,
                TeamRed = teamRed,
                TeamBlueId = teamRed.Id,
                TeamBlue = teamRed,
            };

            var teamRedScore = teamRed.PlayerOne.Score + teamRed.PlayerTwo.Score;
            var teamBlueScore = teamBlue.PlayerOne.Score + teamBlue.PlayerTwo.Score;
            var dif = match.ScoreDiff = teamRedScore - teamBlueScore;
            if (dif >= 0)
            {
                if (dif > 4) dif = 4;
                match.EndGoalsTeamRed = match.StartGoalsTeamRed = dif;
            }
            else
            {
                if (-dif > 4) dif = -4;
                match.EndGoalsTeamBlue = match.StartGoalsTeamBlue = -dif;
                match.ScoreDiff = -match.ScoreDiff;
            }

            return match;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Match GetMatch(int id)
        {
            var match = _dbContext.Matches
                .Include(m => m.TeamBlue.PlayerOne)
                .Include(m => m.TeamBlue.PlayerTwo)
                .Include(m => m.TeamRed.PlayerOne)
                .Include(m => m.TeamRed.PlayerTwo)
                .FirstOrDefault(m => m.Id == id);
            return match;
        }
    }
}
