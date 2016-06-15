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

        public Team GetTeam(int teamId)
        {
            var team = _dbContext
                .Teams
                .Include(p => p.MatchTeam.Select(m => m.Match))
                .FirstOrDefault(p => p.Id == teamId);
            return team;
        }

        public Player GetPlayer(int playerId)
        {
            var player = _dbContext
                .Players
                .Include(p=>p.Teams)
                .Include(p=>p.MatchPlayer.Select(m=>m.Match))
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
                StartTime = DateTime.UtcNow,
                EndTime = null,
                TimeSpan = null,
            };
            _dbContext.Matches.Add(match);
            SaveChanges();

            var TeamRed = new MatchTeam
            {
                IsRedTeam = true,
                MatchId = match.Id,
                Match = match,
                TeamId = teamRed.Id,
                Team = teamRed,
                Score = teamRed.PlayerOne.Score + teamRed.PlayerTwo.Score
            };
            var TeamRedPlayerOne = new MatchPlayer
            {
                IsRedTeam = true,
                IsPlayerOne = true,
                MatchId = match.Id,
                Match = match,
                Player = teamRed.PlayerOne,
                PlayerId = teamRed.PlayerOne.Id,
                Score = teamRed.PlayerOne.Score
            };
            var TeamRedPlayerTwo = new MatchPlayer
            {
                IsRedTeam = true,
                IsPlayerOne = false,
                MatchId = match.Id,
                Match = match,
                Player = teamRed.PlayerTwo,
                PlayerId = teamRed.PlayerTwo.Id,
                Score = teamRed.PlayerTwo.Score
            };

            var TeamBlue = new MatchTeam
            {
                IsRedTeam = false,
                MatchId = match.Id,
                Match = match,
                TeamId = teamBlue.Id,
                Team = teamBlue,
                Score = teamBlue.PlayerOne.Score + teamBlue.PlayerTwo.Score
            };
            var TeamBluePlayerOne = new MatchPlayer
            {
                IsRedTeam = false,
                IsPlayerOne = true,
                MatchId = match.Id,
                Match = match,
                Player = teamBlue.PlayerOne,
                PlayerId = teamBlue.PlayerOne.Id,
                Score = teamBlue.PlayerOne.Score
            };
            var TeamBluePlayerTwo = new MatchPlayer
            {
                IsRedTeam = false,
                IsPlayerOne = false,
                MatchId = match.Id,
                Match = match,
                Player = teamBlue.PlayerTwo,
                PlayerId = teamBlue.PlayerTwo.Id,
                Score = teamBlue.PlayerTwo.Score
            };

            var dif = match.ScoreDiff = TeamRed.Score - TeamBlue.Score;
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

            _dbContext.MatchTeams.Add(TeamRed);
            _dbContext.MatchPlayers.Add(TeamRedPlayerOne);
            _dbContext.MatchPlayers.Add(TeamRedPlayerTwo);
            _dbContext.MatchTeams.Add(TeamBlue);
            _dbContext.MatchPlayers.Add(TeamBluePlayerOne);
            _dbContext.MatchPlayers.Add(TeamBluePlayerTwo);
            SaveChanges();
            //match.TeamRedId = match.TeamRed.TeamId;
            //match.TeamRedPlayerOneId = match.TeamRedPlayerOne.PlayerId;
            //match.TeamRedPlayerTwoId = match.TeamRedPlayerTwo.PlayerId;
            //match.TeamBlueId = match.TeamBlue.TeamId;
            //match.TeamBluePlayerOneId = match.TeamBluePlayerOne.PlayerId;
            //match.TeamBluePlayerTwoId = match.TeamBluePlayerTwo.PlayerId;
            
            return match;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Match GetMatch(int id)
        {
            var match = _dbContext.Matches
                .Include(m=>m.MatchTeam.Select(t=>t.Team))
                .Include(m=>m.MatchPlayer.Select(t=>t.Player))
                .FirstOrDefault(m => m.Id == id);
            return match;
        }
    }
}
