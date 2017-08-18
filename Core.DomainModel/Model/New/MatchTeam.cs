using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model.New
{
    public class MatchTeam : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public int Goals { get; set; }
        public int Handicap { get; set; }
        public bool IsRedTeam { get; set; }

        // Foreign keys
        public int MatchId { get; set; }
        public int TeamId { get; set; }

        // Navigation properties 
        public virtual Match Match { get; set; }
        public virtual Team Team { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
