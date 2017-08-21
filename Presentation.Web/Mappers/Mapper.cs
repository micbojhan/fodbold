﻿using System;
using System.Linq;
using Core.DomainModel.Model.New;
using Presentation.Web.ViewModels;


namespace Presentation.Web.Mappers
{
    public class ManuelMapper
    {
        public PlayerViewModel ToViewModel(Player model, int take = 5)
        {
            var teams = model.TwoTeams.ToList();
            var teams1 = model.OneTeams.ToList();
            teams.AddRange(teams1);
            var matches = teams.SelectMany(t=>t.BlueMatches).ToList();
            var matches1 = teams.SelectMany(t=>t.RedMatches).ToList();
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
                Form = matches.OrderByDescending(m=>m.Id).Take(take).Select(b => b.RedDrawBlueGameResult).ToList(),
                Matches = matches.Select(ToViewModel).ToList(),
                Teams = teams.Select(ToViewModel).ToList(),
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
            var matches1 = model.BlueMatches.ToList();
            var matches2 = model.RedMatches.ToList();
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
                PlayerOne = ToViewModel(model.PlayerOne),
                PlayerTwo = ToViewModel(model.PlayerTwo),
                Form = matches1.OrderByDescending(m => m.Id).Take(5).Select(b => b.RedDrawBlueGameResult).ToList(),
                Matches = matches1.Select(ToViewModel).ToList(),
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
                TeamRedId = model.RedTeamId,
                TeamBlueId = model.BlueTeamId,
                TeamRed = ToViewModel(model.RedTeam),
                TeamBlue = ToViewModel(model.BlueTeam),
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