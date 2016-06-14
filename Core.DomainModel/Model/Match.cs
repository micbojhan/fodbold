using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class Match : IEntity, ICreatedOn, IModifiedOn
    {
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

        // Navigation properties 
        public virtual MatchTeam TeamRed { get; set; }
        public virtual MatchTeam TeamBlue { get; set; }
        public virtual MatchPlayer TeamRedPlayerOne { get; set; }
        public virtual MatchPlayer TeamRedPlayerTwo { get; set; }
        public virtual MatchPlayer TeamBluePlayerOne { get; set; }
        public virtual MatchPlayer TeamBluePlayerTwo { get; set; }

        // Foreign key 
        [ForeignKey("TeamRed"), Column(Order = 0)]
        public int? TeamRedId { get; set; }
        [ForeignKey("TeamBlue"), Column(Order = 1)]
        public int? TeamBlueId { get; set; }
        [ForeignKey("TeamRedPlayerOne"), Column(Order = 2)]
        public int? TeamRedPlayerOneId { get; set; }
        [ForeignKey("TeamRedPlayerTwo"), Column(Order = 3)]
        public int? TeamRedPlayerTwoId { get; set; }
        [ForeignKey("TeamBluePlayerOne"), Column(Order = 4)]
        public int? TeamBluePlayerOneId { get; set; }
        [ForeignKey("TeamBluePlayerTwo"), Column(Order = 5)]
        public int? TeamBluePlayerTwoId { get; set; }

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
