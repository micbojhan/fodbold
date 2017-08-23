using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.DomainModel.Model.New;
using Core.DomainServices;


namespace Infrastructure.DataAccess.Repositorys
{
    public class FussballRepository : IFussballRepository
    {
        private readonly ApplicationContext _dbContext;

        public FussballRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        public List<Player> GetPlayerList()
        {
            var players = _dbContext.Players
                .Include(p => p.OneTeams.Select(c => c.BlueMatches))
                .Include(p => p.OneTeams.Select(c => c.RedMatches))
                .Include(p => p.TwoTeams.Select(c => c.BlueMatches))
                .Include(p => p.TwoTeams.Select(c => c.RedMatches))
                .OrderBy(p => p.Score)
                .ThenBy(p => p.AllTimeHigh)
                .ThenBy(p => p.Name)
                .Where(p => p.OneTeams.Any(t => t.BlueMatches.Any(m => m.Done) || t.RedMatches.Any(m => m.Done)) ||
                            p.TwoTeams.Any(t => t.BlueMatches.Any(m => m.Done) || t.RedMatches.Any(m => m.Done)))
                .ToList();
            return players;
        }

        public List<Player> GetPlayers()
        {
            var players = _dbContext.Players.ToList();
            return players;
        }

        public List<Team> GetTeamList()
        {
            var teams = _dbContext.Teams
                .Include(p => p.BlueMatches)
                .Include(p => p.RedMatches)
                .Include(p => p.PlayerOne)
                .Include(p => p.PlayerTwo)
                .OrderBy(p => p.Score)
                .ThenBy(p => p.AllTimeHigh)
                .Where(p => p.BlueMatches.Any(m => m.Done) || p.RedMatches.Any(m => m.Done))
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
                .Include(p => p.BlueMatches)
                .Include(p => p.RedMatches)
                .Include(p => p.PlayerOne)
                .Include(p => p.PlayerTwo)
                .FirstOrDefault(p => p.Id == teamId);
            return team;
        }

        public Player GetPlayer(int playerId)
        {
            var player = _dbContext.Players
                .Include(p => p.TwoTeams.Select(t => t.RedMatches))
                .Include(p => p.TwoTeams.Select(t => t.BlueMatches))
                .Include(p => p.OneTeams.Select(t => t.RedMatches))
                .Include(p => p.OneTeams.Select(t => t.BlueMatches))
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

        public Season getLastSeason()
        {
            return _dbContext.Seasons.FirstOrDefault();
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
            var season = getLastSeason();

            var match = new Match
            {
                StartTime = DateTime.UtcNow,
                EndTime = null,
                TimeSpan = null,
                RedTeamId = teamRed.Id,
                RedTeam = teamRed,
                BlueTeamId = teamBlue.Id,
                BlueTeam = teamBlue,
                SeasonId = season.Id,
                Season = season
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
            _dbContext.Matches.Add(match);
            return match;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Match GetMatch(int id)
        {
            var match = _dbContext.Matches
                .Include(m => m.BlueTeam.PlayerOne)
                .Include(m => m.BlueTeam.PlayerTwo)
                .Include(m => m.RedTeam.PlayerOne)
                .Include(m => m.RedTeam.PlayerTwo)
                .FirstOrDefault(m => m.Id == id);
            return match;
        }
    }
}
