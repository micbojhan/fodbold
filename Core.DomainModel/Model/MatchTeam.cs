using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class MatchTeam : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int GameResult { get; set; }

        // Navigation properties 
        public virtual ICollection<Match> Match { get; set; }
        public virtual Team Team { get; set; }

        // Foreign keys
        [ForeignKey("Team"), Column(Order = 0)]
        public int? TeamId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
