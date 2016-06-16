using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model
{
    public class Match : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public bool Done { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? TimeSpan { get; set; }

        public int ScoreDiff { get; set; }

        public int StartGoalsTeamRed { get; set; }
        public int EndGoalsTeamRed { get; set; }

        public int StartGoalsTeamBlue { get; set; }
        public int EndGoalsTeamBlue { get; set; }

        public int GameResult { get; set; } // 1X2 - Red-Draw-Blue

        // Navigation properties 
        [ForeignKey("TeamRedId")]
        public virtual Team TeamRed { get; set; }
        [ForeignKey("TeamBlueId")]
        public virtual Team TeamBlue { get; set; }

        // Foreign key 
        [Key, Column(Order = 0)]
        public int? TeamRedId { get; set; }
        [Key, Column(Order = 1)]
        public int? TeamBlueId { get; set; }

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
