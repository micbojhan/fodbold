using System;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class Test : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}