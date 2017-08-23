using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Web.ViewModels
{
    public class CreatePlayerViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Initials { get; set; }
        public int Score { get; set; }
    }
}