using System;
using System.ComponentModel.DataAnnotations;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model.Old
{
    public class Test : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}