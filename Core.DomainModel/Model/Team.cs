using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class Team : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public string Name { get; set; }
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

        // Foreign key 
        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }

        // Navigation properties 
        public virtual Player PlayerOne { get; set; }
        public virtual Player PlayerTwo { get; set; }

        // Navigation property
        public virtual ICollection<Derby> Derbies { get; set; }
        public virtual ICollection<MatchTeam> MatchTeam { get; set; }
        
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}


/*

CREATE TABLE [dbo].[Team] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [PlayerOneId] INT           NOT NULL,
    [PlayerTwoId] INT           NOT NULL,
    [Won]         INT           NOT NULL,
    [Lost]        INT           NOT NULL,
    [Score]       INT           NOT NULL,
    [AllTimeHigh] INT           NOT NULL,
    [AllTimeLow]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PlayerOne] FOREIGN KEY ([PlayerOneId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_PlayerTwo] FOREIGN KEY ([PlayerTwoId]) REFERENCES [Player]([Id])
);





*/
