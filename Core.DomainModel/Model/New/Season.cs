using System;
using System.Collections.Generic;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model.New
{
    public class Season : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        // Dates
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

