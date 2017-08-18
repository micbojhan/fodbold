using System;
using System.ComponentModel.DataAnnotations;
using Core.DomainModel.Interfaces;

namespace Core.DomainModel.Model.New
{
    public class Match : IEntity, ICreatedOn, IModifiedOn
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        // Navigation properties 
        public virtual MatchTeam TeamRed { get; set; }
        public virtual MatchTeam TeamBlue { get; set; }

        // Foreign key 
        public int TeamRedId { get; set; }
        public int TeamBlueId { get; set; }

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
