using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Web.ViewModels
{
    public class CreatePlayerViewModel
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public int Score { get; set; }
    }
}