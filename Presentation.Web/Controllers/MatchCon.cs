using System;
using System.Collections.Generic;
using System.Linq;
using Core.DomainModel.Enums;
using Core.DomainModel.Model.New;
using Presentation.Web.ViewModels;

namespace Presentation.Web.Controllers
{
    public class MatchCon
    {
        public CreateMatchViewModel CreateMatchViewModel(CreateMatchViewModel vm, List<Player> playerToMatch)
        {
            var model = new CreateMatchViewModel
            {
                FixedTeam = vm.FixedTeam,
                HandicapGame = vm.HandicapGame,
            };

            if (vm.FixedTeam)
            {
                model.PlayerOneId = playerToMatch.First(p => p.Initials == vm.PlayerOne).Id;
                model.PlayerTwoId = playerToMatch.First(p => p.Initials == vm.PlayerTwo).Id;
                model.PlayerThreeId = playerToMatch.First(p => p.Initials == vm.PlayerThree).Id;
                model.PlayerFourId = playerToMatch.First(p => p.Initials == vm.PlayerFour).Id;
            }
            else
            {   // Sortede by player score
                model.PlayerOneId = playerToMatch.ElementAt(0).Id;
                model.PlayerTwoId = playerToMatch.ElementAt(3).Id;
                model.PlayerThreeId = playerToMatch.ElementAt(1).Id;
                model.PlayerFourId = playerToMatch.ElementAt(2).Id;
            }
            return model;
        }

        public Match SetResult(Match m, int redGoals, int blueGoals)
        {
            m.Done = true;
            m.EndTime = DateTime.UtcNow;
            m.EndGoalsTeamBlue = blueGoals;
            m.EndGoalsTeamRed = redGoals;
            m.TimeSpan = m.EndTime - m.StartTime;
            SetMatchWinner(m);

            return m;
        }

        private void SetMatchWinner(Match m)
        {
            int blueTeamGoals = m.EndGoalsTeamBlue - m.StartGoalsTeamBlue;
            int redTeamGoals = m.EndGoalsTeamRed - m.StartGoalsTeamRed;
            int blueTeam;
            int redTeam;
            if (m.EndGoalsTeamBlue > m.EndGoalsTeamRed)
            {
                m.RedDrawBlueGameResult = (int)RedDrawBlueGameResultEnum.Blue;
                blueTeam = (int)MatchResultEnum.Won;
                redTeam = (int)MatchResultEnum.Lost;
            }
            else if (m.EndGoalsTeamBlue < m.EndGoalsTeamRed)
            {
                m.RedDrawBlueGameResult = (int)RedDrawBlueGameResultEnum.Red;
                blueTeam = (int)MatchResultEnum.Lost;
                redTeam = (int)MatchResultEnum.Won;
            }
            else
            {
                m.RedDrawBlueGameResult = (int)RedDrawBlueGameResultEnum.Draw;
                blueTeam = (int)MatchResultEnum.Draw;
                redTeam = (int)MatchResultEnum.Draw;
            }

            SetTeamWinner(m.BlueTeam, blueTeam, m.EndGoalsTeamBlue, blueTeamGoals, m.EndGoalsTeamRed, redTeamGoals);
            SetPlayerWinner(m.BlueTeam.PlayerOne, blueTeam, m.EndGoalsTeamBlue, blueTeamGoals, m.EndGoalsTeamRed, redTeamGoals);
            SetPlayerWinner(m.BlueTeam.PlayerTwo, blueTeam, m.EndGoalsTeamBlue, blueTeamGoals, m.EndGoalsTeamRed, redTeamGoals);

            SetTeamWinner(m.RedTeam, redTeam, m.EndGoalsTeamRed, redTeamGoals, m.EndGoalsTeamBlue, blueTeamGoals);
            SetPlayerWinner(m.RedTeam.PlayerOne, redTeam, m.EndGoalsTeamRed, redTeamGoals, m.EndGoalsTeamBlue, blueTeamGoals);
            SetPlayerWinner(m.RedTeam.PlayerTwo, redTeam, m.EndGoalsTeamRed, redTeamGoals, m.EndGoalsTeamBlue, blueTeamGoals);
        }



        private void SetTeamWinner(Team t, int iWinDrawLost, int scoredWithHc, int scored, int againstWithHc, int against)
        {
            t.GoalsScored += scored;
            t.GoalsScoredHc += scoredWithHc;
            t.GoalsAgainst += against;
            t.GoalsAgainstHc += againstWithHc;
            switch ((MatchResultEnum)iWinDrawLost)
            {
                case MatchResultEnum.Won:
                    {
                        --t.Score;
                        ++t.Won;
                        if (t.Score < t.AllTimeHigh) t.AllTimeHigh = t.Score;
                        if (t.Score > t.AllTimeLow) t.AllTimeLow = t.Score;
                        break;
                    }
                case MatchResultEnum.Lost:
                    {
                        ++t.Score;
                        ++t.Lost;
                        if (t.Score < t.AllTimeHigh) t.AllTimeHigh = t.Score;
                        if (t.Score > t.AllTimeLow) t.AllTimeLow = t.Score;
                        break;
                    }
                case MatchResultEnum.Draw:
                    {
                        ++t.Draw;
                        break;
                    }
            }
            //SetPlayerWinner(t.PlayerOne, iWinDrawLost, scoredWithHc, scored, againstWithHc, against);
            //SetPlayerWinner(t.PlayerTwo, iWinDrawLost, scoredWithHc, scored, againstWithHc, against);
        }

        private void SetPlayerWinner(Player p, int iWinDrawLost , int scoredWithHc, int scored, int againstWithHc, int against)
        {
            p.GoalsScored += scored;
            p.GoalsScoredHc += scoredWithHc;
            p.GoalsAgainst += against;
            p.GoalsAgainstHc += againstWithHc;
            switch ((MatchResultEnum)iWinDrawLost)
            {
                case MatchResultEnum.Won:
                    {
                        --p.Score;
                        ++p.Won;
                        if (p.Score < p.AllTimeHigh) p.AllTimeHigh = p.Score;
                        if (p.Score > p.AllTimeLow) p.AllTimeLow = p.Score;
                        break;
                    }
                case MatchResultEnum.Lost:
                    {
                        ++p.Score;
                        ++p.Lost;
                        if (p.Score < p.AllTimeHigh) p.AllTimeHigh = p.Score;
                        if (p.Score > p.AllTimeLow) p.AllTimeLow = p.Score;
                        break;
                    }
                case MatchResultEnum.Draw:
                    {
                        ++p.Draw;
                        break;
                    }
            }
        }
    }
}