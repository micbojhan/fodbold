using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model.New
{
    public class Match : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        // Info
        public bool Done { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? TimeSpan { get; set; }
        public int ScoreDiff { get; set; }
        public int StartGoalsTeamRed { get; set; }
        public int EndGoalsTeamRed { get; set; }
        public int StartGoalsTeamBlue { get; set; }
        public int EndGoalsTeamBlue { get; set; }
        public int RedDrawBlueGameResult { get; set; } // 1X2 - Red-Draw-Blue

        // Foreign keys
        public int SeasonId { get; set; }
        public int RedTeamId { get; set; }
        public int BlueTeamId { get; set; }

        // Navigation properties 
        public virtual Team RedTeam { get; set; }
        public virtual Team BlueTeam { get; set; }
        public virtual Season Season { get; set; }

        // Dates
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
