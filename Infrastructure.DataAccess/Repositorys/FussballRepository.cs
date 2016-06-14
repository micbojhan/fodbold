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
                .Include(p => p.MatchPlayer.Select(c => c.Match))
                .OrderBy(p => p.Score)
                .ThenBy(p => p.AllTimeHigh)
                .ThenBy(p => p.Name)
                .ToList();
            return players;
        }

        public List<Team> GetTeamList()
        {
            var teams = _dbContext.Teams
                .Include(p => p.MatchTeam.Select(c => c.Match))
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



        public Player GetPlayer(int playerId)
        {
            var player = _dbContext.Players.FirstOrDefault(p => p.Id == playerId);
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

        public Team GetTeam(int playerOneId, int playerTwoId)
        {
            var team = _dbContext.Teams
                .Include(t => t.PlayerOne)
                .Include(t => t.PlayerTwo)
                .FirstOrDefault(t =>
                    (t.PlayerOneId == playerOneId && t.PlayerTwoId == playerTwoId) ||
                    (t.PlayerTwoId == playerOneId && t.PlayerOneId == playerTwoId));
            return team;
        }

        public Match CreateMatch(int playerOneId, int playerTwoId, int playerThreeId, int playerFourId)
        {
            var teamRed = GetTeam(playerOneId, playerTwoId);
            var teamBlue = GetTeam(playerThreeId, playerFourId);

            var match = new Match
            {
                MatchGuid = Guid.NewGuid(),
                StartTime = DateTime.UtcNow,
                EndTime = null,
                TimeSpan = null,
                TeamRed = new MatchTeam
                {
                    TeamId = teamRed.Id,
                    Team = teamRed,
                    Score = teamRed.PlayerOne.Score + teamRed.PlayerTwo.Score
                },
                TeamRedPlayerOne = new MatchPlayer
                {
                    Player = teamRed.PlayerOne,
                    PlayerId = teamRed.PlayerOne.Id,
                    Score = teamRed.PlayerOne.Score
                },
                TeamRedPlayerTwo = new MatchPlayer
                {
                    Player = teamRed.PlayerTwo,
                    PlayerId = teamRed.PlayerTwo.Id,
                    Score = teamRed.PlayerTwo.Score
                },
                TeamBlue = new MatchTeam
                {
                    TeamId = teamBlue.Id,
                    Team = teamBlue,
                    Score = teamBlue.PlayerOne.Score + teamBlue.PlayerTwo.Score
                },
                TeamBluePlayerOne = new MatchPlayer
                {
                    Player = teamBlue.PlayerOne,
                    PlayerId = teamBlue.PlayerOne.Id,
                    Score = teamBlue.PlayerOne.Score
                },
                TeamBluePlayerTwo = new MatchPlayer
                {
                    Player = teamBlue.PlayerTwo,
                    PlayerId = teamBlue.PlayerTwo.Id,
                    Score = teamBlue.PlayerTwo.Score
                }
            };

            var dif = match.ScoreDiff = match.TeamRed.Score - match.TeamBlue.Score;
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

            _dbContext.MatchTeams.Add(match.TeamRed);
            _dbContext.MatchPlayers.Add(match.TeamRedPlayerOne);
            _dbContext.MatchPlayers.Add(match.TeamRedPlayerTwo);
            _dbContext.MatchTeams.Add(match.TeamBlue);
            _dbContext.MatchPlayers.Add(match.TeamBluePlayerOne);
            _dbContext.MatchPlayers.Add(match.TeamBluePlayerTwo);
            SaveChanges();
            match.TeamRedId = match.TeamRed.TeamId;
            match.TeamRedPlayerOneId = match.TeamRedPlayerOne.PlayerId;
            match.TeamRedPlayerTwoId = match.TeamRedPlayerTwo.PlayerId;
            match.TeamBlueId = match.TeamBlue.TeamId;
            match.TeamBluePlayerOneId = match.TeamBluePlayerOne.PlayerId;
            match.TeamBluePlayerTwoId = match.TeamBluePlayerTwo.PlayerId;
            _dbContext.Matches.Add(match);
            return match;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Match GetMatch(Guid matchGuid)
        {
            var match = _dbContext.Matches.FirstOrDefault(m => m.MatchGuid == matchGuid);
            return match;
        }
    }
}
