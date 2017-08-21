//using System;
//using Core.DomainModel.Interfaces;

//namespace Core.DomainModel.Model.Old
//{
//    public class Derby : IEntity, ICreatedOn, IModifiedOn
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }

//        // Foreign key 
//        public int TeamOneId { get; set; }
//        public int TeamTwoId { get; set; }

//        // Navigation properties 
//        public virtual Team TeamOne { get; set; }
//        public virtual Team TeamTwo { get; set; }

//        public DateTime CreatedOn { get; set; }
//        public DateTime ModifiedOn { get; set; }
//    }
//}

///*

//CREATE TABLE [dbo].Derby (
//	[Id]            INT             IDENTITY (1, 1) NOT NULL,
//    [Name]          NVARCHAR(50)    NOT NULL, 
//    [TeamOneId]     INT             NOT NULL, 
//    [TeamTwoId]     INT             NOT NULL,
//    PRIMARY KEY CLUSTERED ([Id] ASC), 
//    CONSTRAINT [FK_TeamOne] FOREIGN KEY ([TeamOneId]) REFERENCES [Team]([Id]), 
//    CONSTRAINT [FK_TeamTwo] FOREIGN KEY ([TeamTwoId]) REFERENCES [Team]([Id])
//);

//*/
