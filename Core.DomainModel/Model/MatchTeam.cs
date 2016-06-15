using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class MatchTeam : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public int GameResult { get; set; }
        public bool IsRedTeam { get; set; }


        // Foreign keys
        [Key, Column(Order = 0)]
        public int MatchId { get; set; }
        [Key, Column(Order = 1)]
        public int TeamId { get; set; }

        // Navigation properties 
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
