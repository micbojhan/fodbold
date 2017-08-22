using System;
using System.Linq;
using Core.DomainModel.Enums;
using Core.DomainModel.Model.New;
using Presentation.Web.ViewModels;


namespace Presentation.Web.Mappers
{
    public class ManuelMapper
    {
        public PlayerViewModel ToViewModel(Player model, int subNiveau = 3, int take = 5)
        {
            if (subNiveau == 0 || model == null) return null;
            var teams = model.TwoTeams?.ToList();
            var teams1 = model.OneTeams?.ToList();
            teams.AddRange(teams1);
            var matches = teams?.SelectMany(t => t.BlueMatches).Where(m => m.Done).ToList();
            var matches1 = teams?.SelectMany(t => t.RedMatches).Where(m => m.Done).ToList();
            matches.AddRange(matches1);
            //matches.Where(m=>m.TeamRed.PlayerOneId == model.Id)

            var viewModel = new PlayerViewModel
            {
                Id = model.Id,
                Name = model.Name,
                NickName = model.NickName,
                FullName = model.FullName,
                Initials = model.Initials,
                Won = model.Won,
                Draw = model.Draw,
                Lost = model.Lost,
                Score = model.Score,
                AllTimeHigh = model.AllTimeHigh,
                AllTimeLow = model.AllTimeLow,
                GoalsScored = model.GoalsScored,
                GoalsScoredHc = model.GoalsScoredHc,
                GoalsAgainst = model.GoalsAgainst,
                GoalsAgainstHc = model.GoalsAgainstHc,
                Form = matches.OrderByDescending(m => m.Id).Take(take).Select(b => ToPlayerForm(model.Id, b)).ToList(),
                Matches = matches.Select(m => ToViewModel(m, subNiveau - 1)).ToList(),
                Teams = teams.Select(m => ToViewModel(m, subNiveau - 1)).ToList(),
            };
            return viewModel;
        }

        public int ToPlayerForm(int playerId, Match match)
        {
            if (match.BlueTeam.PlayerOneId == playerId || match.BlueTeam.PlayerTwoId == playerId)
                return ToTeamForm(match.BlueTeamId, match);
            else if (match.RedTeam.PlayerOneId == playerId || match.RedTeam.PlayerTwoId == playerId)
                return ToTeamForm(match.RedTeamId, match);
            else
                return 99;
        }

        public Player ToDomain(PlayerViewModel viewModel)
        {
            var model = new Player
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                NickName = viewModel.NickName,
                FullName = viewModel.FullName,
                Initials = viewModel.Initials,
                Won = viewModel.Won,
                Draw = viewModel.Draw,
                Lost = viewModel.Lost,
                Score = viewModel.Score,
                AllTimeHigh = viewModel.AllTimeHigh,
                AllTimeLow = viewModel.AllTimeLow,
            };
            return model;
        }

        public TeamViewModel ToViewModel(Team model, int subNiveau = 3)
        {
            if (subNiveau == 0 || model == null) return null;
            var matches1 = model.BlueMatches?.Where(m => m.Done).ToList();
            var matches2 = model.RedMatches?.Where(m => m.Done).ToList();
            matches1.AddRange(matches2);

            var viewModel = new TeamViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Won = model.Won,
                Draw = model.Draw,
                Lost = model.Lost,
                Score = model.Score,
                AllTimeHigh = model.AllTimeHigh,
                AllTimeLow = model.AllTimeLow,
                GoalsScored = model.GoalsScored,
                GoalsScoredHc = model.GoalsScoredHc,
                GoalsAgainst = model.GoalsAgainst,
                GoalsAgainstHc = model.GoalsAgainstHc,
                PlayerOneId = model.PlayerOneId,
                PlayerTwoId = model.PlayerTwoId,
                PlayerOne = ToViewModel(model.PlayerOne, subNiveau - 1),
                PlayerTwo = ToViewModel(model.PlayerTwo, subNiveau - 1),
                Form = matches1.OrderByDescending(m => m.Id).Take(5).Select(b => ToTeamForm(model.Id, b)).ToList(),
                Matches = matches1.Select(m => ToViewModel(m, subNiveau - 1)).ToList(),
            };
            return viewModel;
        }

        public int ToTeamForm(int teamId, Match match)
        {
            if (teamId == match.BlueTeamId && match.RedDrawBlueGameResult == (int)RedDrawBlueGameResultEnum.Blue)
                return (int)MatchResultEnum.Won;
            else if (teamId == match.RedTeamId && match.RedDrawBlueGameResult == (int)RedDrawBlueGameResultEnum.Red)
                return (int)MatchResultEnum.Won;
            else if (match.RedDrawBlueGameResult == (int)RedDrawBlueGameResultEnum.Draw)
                return (int)MatchResultEnum.Draw;
            else
                return (int)MatchResultEnum.Lost;
        }

        public Team ToDomain(TeamViewModel viewModel)
        {
            var model = new Team
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Won = viewModel.Won,
                Draw = viewModel.Draw,
                Lost = viewModel.Lost,
                Score = viewModel.Score,
                AllTimeHigh = viewModel.AllTimeHigh,
                AllTimeLow = viewModel.AllTimeLow,
                PlayerOneId = viewModel.PlayerOneId,
                PlayerTwoId = viewModel.PlayerTwoId,
                PlayerOne = ToDomain(viewModel.PlayerOne),
                PlayerTwo = ToDomain(viewModel.PlayerTwo),
            };
            return model;
        }


        public MatchViewModel ToViewModel(Match model, int subNiveau = 3)
        {
            if (subNiveau == 0 || model == null) return null;
            var matchModel = new MatchViewModel
            {
                Id = model.Id,
                Done = model.Done,
                StartTime = model.StartTime,
                StartTimeStr = model.StartTime.HasValue ? TimeZone.CurrentTimeZone.ToLocalTime(model.StartTime.Value).ToString("dd/MM HH:mm") : null,
                EndTime = model.EndTime,
                EndTimeStr = model.EndTime.HasValue ? TimeZone.CurrentTimeZone.ToLocalTime(model.EndTime.Value).ToString("dd/MM HH:mm") : null,
                TimeSpan = model.TimeSpan,
                TimeSpanStr = model.TimeSpan?.ToString(@"mm\:ss"),
                ScoreDiff = model.ScoreDiff,
                GoalsTeamRed = model.EndGoalsTeamRed - model.StartGoalsTeamRed,
                StartGoalsTeamRed = model.StartGoalsTeamRed,
                EndGoalsTeamRed = model.EndGoalsTeamRed,
                GoalsTeamBlue = model.EndGoalsTeamBlue - model.StartGoalsTeamBlue,
                StartGoalsTeamBlue = model.StartGoalsTeamBlue,
                EndGoalsTeamBlue = model.EndGoalsTeamBlue,
                TeamRedId = model.RedTeamId,
                TeamBlueId = model.BlueTeamId,
                TeamRed = ToViewModel(model.RedTeam, subNiveau - 1),
                TeamBlue = ToViewModel(model.BlueTeam, subNiveau - 1),
                GameResult = model.RedDrawBlueGameResult,
                ScoreTeamRed = model.RedTeam.Score,
                ScoreTeamBlue = model.BlueTeam.Score,
            };


            return matchModel;
        }

        public Match ToDomain(MatchViewModel viewModel)
        {
            var model = new Match
            {
                Id = viewModel.Id,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                TimeSpan = viewModel.TimeSpan,
                StartGoalsTeamRed = viewModel.StartGoalsTeamRed,
                EndGoalsTeamRed = viewModel.EndGoalsTeamRed,
                StartGoalsTeamBlue = viewModel.StartGoalsTeamBlue,
                EndGoalsTeamBlue = viewModel.EndGoalsTeamBlue,
                RedTeamId = viewModel.TeamRedId,
                BlueTeamId = viewModel.TeamBlueId,
                RedTeam = ToDomain(viewModel.TeamRed),
                BlueTeam = ToDomain(viewModel.TeamBlue),
                RedDrawBlueGameResult = viewModel.GameResult,
            };
            return model;
        }

    }
}