using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class MatchPlayer : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public int GameResult { get; set; }
        public bool IsPlayerOne { get; set; }
        public bool IsRedTeam { get; set; }

        // Navigation properties 
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        // Foreign keys
        public int PlayerId { get; set; }
        public int MatchId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
