using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class MatchPlayer : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int GameResult { get; set; }

        // Navigation properties 
        public virtual Match Match { get; set; }
        public virtual Player Player { get; set; }

        // Foreign keys
        public int PlayerId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
