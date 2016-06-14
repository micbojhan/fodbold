using System.Collections.Generic;

namespace Presentation.Web.ViewModels
{
    public class TeamViewModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
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


        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }
        public PlayerViewModel PlayerOne { get; set; }
        public PlayerViewModel PlayerTwo { get; set; }



        public List<int> Form { get; set; }

    }
}