using System.Collections.Generic;

namespace Presentation.Web.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int Score { get; set; }
        public int AllTimeHigh { get; set; }
        public int AllTimeLow { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsScoredHc { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalsAgainstHc { get; set; }


        public List<int> Form { get; set; }
        public List<MatchViewModel> Matches { get; set; }
        public List<TeamViewModel> Teams { get; set; }

    }
}