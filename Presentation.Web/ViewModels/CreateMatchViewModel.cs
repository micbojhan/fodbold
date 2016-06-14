using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Web.ViewModels
{
    public class CreateMatchViewModel
    {
        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }
        public int PlayerThreeId { get; set; }
        public int PlayerFourId { get; set; }

        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string PlayerThree { get; set; }
        public string PlayerFour { get; set; }
        public bool HandicapGame { get; set; }
        public bool FixedTeam { get; set; }
    }
}