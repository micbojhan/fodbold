using System;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class GameResult : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
