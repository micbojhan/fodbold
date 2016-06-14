using System.Collections.Generic;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.OldModel
{
    public class Teacher : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
