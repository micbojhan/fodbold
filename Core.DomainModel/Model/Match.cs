using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class Match : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public Guid MatchGuid { get; set; }
        public bool Done { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? TimeSpan { get; set; }

        public int ScoreDiff { get; set; }
        public int StartGoalsTeamRed { get; set; }
        public int EndGoalsTeamRed { get; set; }
        public int StartGoalsTeamBlue { get; set; }
        public int EndGoalsTeamBlue { get; set; }

        public int TeamResult { get; set; } // 1X2 - Red-Draw-Blue

        public virtual ICollection<MatchTeam> MatchTeam { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayer { get; set; }

        // Navigation properties 
        //public virtual MatchTeam TeamRed { get; set; }
        //public virtual MatchTeam TeamBlue { get; set; }
        //public virtual MatchPlayer TeamRedPlayerOne { get; set; }
        //public virtual MatchPlayer TeamRedPlayerTwo { get; set; }
        //public virtual MatchPlayer TeamBluePlayerOne { get; set; }
        //public virtual MatchPlayer TeamBluePlayerTwo { get; set; }

        // Foreign key 
        //public int TeamRedId { get; set; }
        //public int TeamBlueId { get; set; }
        //public int TeamRedPlayerOneId { get; set; }
        //public int TeamRedPlayerTwoId { get; set; }
        //public int TeamBluePlayerOneId { get; set; }
        //public int TeamBluePlayerTwoId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}


/*

CREATE TABLE [dbo].[Match] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [TeamRedId]     INT NOT NULL,
    [TeamBlueId]    INT NOT NULL,
    [TeamOneScore]  INT NOT NULL,
    [TeamTwoScore]  INT NOT NULL,
    [StartTime]     DATETIME NOT NULL, 
    [EndTime]       DATETIME NOT NULL, 
    [TimeInMinutes] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_TeamRed] FOREIGN KEY ([TeamRedId]) REFERENCES [Team]([Id]), 
    CONSTRAINT [FK_TeamBlue] FOREIGN KEY ([TeamBlueId]) REFERENCES [Team]([Id])
);

*/
