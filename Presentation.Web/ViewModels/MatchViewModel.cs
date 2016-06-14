using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Web.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public Guid MatchGuid { get; set; }
        public bool Done { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? TimeSpan { get; set; }

        public int ScoreDiff { get; set; }
        public int StartGoalsTeamRed { get; set; }
        public int EndGoalsTeamRed { get; set; }
        public int StartGoalsTeamBlue { get; set; }
        public int EndGoalsTeamBlue { get; set; }

        public int TeamResult { get; set; }


        // Xtra values
        public string DerbyName { get; set; }
        public int ScoreTeamRed { get; set; }
        public int ScoreTeamBlue { get; set; }

        public int GoalsTeamRed { get; set; }
        public int GoalsTeamBlue { get; set; }

        public bool HandicapGame { get; set; }
        public bool FixedTeam { get; set; }

        public int TeamRedId { get; set; }
        public int TeamBlueId { get; set; }
        public TeamViewModel TeamRed { get; set; }
        public TeamViewModel TeamBlue { get; set; }
        public string TimeSpanStr { get; set; }
        public string EndTimeStr { get; set; }
        public string StartTimeStr { get; set; }
        public bool TeamBlueWinner { get; set; }
        public bool TeamRedWinner { get; set; }
    }
}