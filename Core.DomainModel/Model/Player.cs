using System;
using System.Collections.Generic;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class Player : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int Score { get; set; }
        public int AllTimeHigh { get; set; }
        public int AllTimeLow { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsAgainstHc { get; set; }
        public int GoalsScoredHc { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayer { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

/*

CREATE TABLE [dbo].[Player] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [NickName]    NVARCHAR (50) NULL,
    [FullName]    NVARCHAR (50) NULL,
    [Initials]    NVARCHAR (5)  NOT NULL,
    [Won]         INT           NOT NULL,
    [Lost]        INT           NOT NULL,
    [Score]       INT           NOT NULL,
    [AllTimeHigh] INT           NOT NULL,
    [AllTimeLow]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

    */
     