using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class TeamPlayer : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        [Key, Column(Order = 0)]
        public int TeamId { get; set; }
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }
        public bool IsPlayerOne { get; set; }

        // Navigation properties 
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
