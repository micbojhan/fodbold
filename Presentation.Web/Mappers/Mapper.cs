using System;
using System.Linq;
using Core.DomainModel.Model;
using Presentation.Web.ViewModels;


namespace Presentation.Web.Mappers
{
    public class ManuelMapper
    {
        public PlayerViewModel ToViewModel(Player model, int take = 5)
        {
            var viewModel = new PlayerViewModel
            {
                PlayerId = model.Id,
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
                Form = model.MatchPlayer.OrderByDescending(i => i.Id).Select(b => b.GameResult).Take(take).ToList()
            };
            return viewModel;
        }

        public Player ToDomain(PlayerViewModel viewModel)
        {
            var model = new Player
            {
                Id = viewModel.PlayerId,
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

        public TeamViewModel ToViewModel(Team model)
        {
            var viewModel = new TeamViewModel
            {
                TeamId = model.Id,
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
                PlayerOneId = model.PlayerOneId.GetValueOrDefault(),
                PlayerTwoId = model.PlayerTwoId.GetValueOrDefault(),
                PlayerOne = ToViewModel(model.PlayerOne),
                PlayerTwo = ToViewModel(model.PlayerTwo),
                Form =  model.MatchTeam.OrderByDescending(i => i.Id).Select(b => b.GameResult).Take(5).ToList()
            };
            return viewModel;
        }

        public Team ToDomain(TeamViewModel viewModel)
        {
            var model = new Team
            {
                Id = viewModel.TeamId,
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


        public MatchViewModel ToViewModel(Match model)
        {
            var matchModel = new MatchViewModel
            {
                MatchId = model.Id,
                MatchGuid = model.MatchGuid,
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
                TeamRedId = model.TeamRedId.GetValueOrDefault(),
                TeamBlueId = model.TeamBlueId.GetValueOrDefault(),
                TeamRed = ToViewModel(model.TeamRed),
                TeamBlue = ToViewModel(model.TeamBlue),
                TeamResult = model.TeamResult,
                ScoreTeamBlue = model.TeamBlue.Score,
                ScoreTeamRed = model.TeamRed.Score,
            };

            return matchModel;
        }

        public Match ToDomain(MatchViewModel viewModel)
        {
            var model = new Match
            {
                Id = viewModel.MatchId,
                MatchGuid = viewModel.MatchGuid,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                TimeSpan = viewModel.TimeSpan,
                StartGoalsTeamRed = viewModel.StartGoalsTeamRed,
                EndGoalsTeamRed = viewModel.EndGoalsTeamRed,
                StartGoalsTeamBlue = viewModel.StartGoalsTeamBlue,
                EndGoalsTeamBlue = viewModel.EndGoalsTeamBlue,
                TeamRedId = viewModel.TeamRedId,
                TeamBlueId = viewModel.TeamBlueId,
                TeamRed = ToDomainMT(viewModel.TeamRed),
                TeamBlue = ToDomainMT(viewModel.TeamBlue),
                TeamResult = viewModel.TeamResult,
            };
            return model;
        }


        public TeamViewModel ToViewModel(MatchTeam model)
        {
            var team = model.Team;
            var viewModel = new TeamViewModel
            {
                TeamId = team.Id,
                Name = team.Name,
                Won = team.Won,
                Draw = team.Draw,
                Lost = team.Lost,
                Score = team.Score,
                AllTimeHigh = team.AllTimeHigh,
                AllTimeLow = team.AllTimeLow,
                PlayerOneId = team.PlayerOneId.GetValueOrDefault(),
                PlayerTwoId = team.PlayerTwoId.GetValueOrDefault(),
                PlayerOne = ToViewModel(team.PlayerOne),
                PlayerTwo = ToViewModel(team.PlayerTwo),
            };
            return viewModel;
        }

        public MatchTeam ToDomainMT(TeamViewModel viewModel)
        {
            var model = new Team
            {
                Id = viewModel.TeamId,
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

            var matchteam = new MatchTeam
            {
                Team = model,
            };

            return matchteam;
        }

    }
}