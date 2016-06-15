using System;
using System.Collections.Generic;
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
                Form = model.MatchPlayer.OrderByDescending(i => i.Id).Select(b => b.GameResult).Take(take).ToList(),
               // Matches = model.MatchPlayer.OrderByDescending(i => i.Id).Select(b => b.GameResult).Take(take).ToList(),
               
            };

            return viewModel;
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

        public TeamViewModel ToViewModel(Team model)
        {
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
                PlayerOneId = model.PlayerOneId.Value,
                PlayerTwoId = model.PlayerTwoId.Value,
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


        public MatchViewModel ToViewModel(Match model)
        {
            
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
                //TeamRedId = model.TeamRedId,
                //TeamBlueId = model.TeamBlueId,
                TeamRed = ToViewModel(model.MatchTeam.FirstOrDefault(t => t.IsRedTeam)),
                TeamBlue = ToViewModel(model.MatchTeam.FirstOrDefault(t => !t.IsRedTeam)),
                TeamResult = model.TeamResult,
                ScoreTeamRed = model.MatchTeam.FirstOrDefault(t => t.IsRedTeam).Score,
                ScoreTeamBlue = model.MatchTeam.FirstOrDefault(t => !t.IsRedTeam).Score,
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
                //TeamRedId = viewModel.TeamRedId,
                //TeamBlueId = viewModel.TeamBlueId,
                //TeamRed = ToDomainMT(viewModel.TeamRed),
                //TeamBlue = ToDomainMT(viewModel.TeamBlue),
                TeamResult = viewModel.TeamResult,
            };
            return model;
        }


        public TeamViewModel ToViewModel(MatchTeam model)
        {
            var team = model.Team;
            var viewModel = new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Won = team.Won,
                Draw = team.Draw,
                Lost = team.Lost,
                Score = team.Score,
                AllTimeHigh = team.AllTimeHigh,
                AllTimeLow = team.AllTimeLow,
                PlayerOneId = team.PlayerOneId.Value,
                PlayerTwoId = team.PlayerTwoId.Value,
                PlayerOne = ToViewModel(team.PlayerOne),
                PlayerTwo = ToViewModel(team.PlayerTwo),
            };
            return viewModel;
        }

        public MatchTeam ToDomainMT(TeamViewModel viewModel)
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

            var matchteam = new MatchTeam
            {
                Team = model,
            };

            return matchteam;
        }

    }
}