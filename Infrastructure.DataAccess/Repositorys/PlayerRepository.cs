﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel.Model;
using Core.DomainModel.Model.New;


namespace Infrastructure.DataAccess.Repositorys
{
    public class PlayerRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Player> _dbSet;

        public PlayerRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<Player>();
        }

        public List<Player> GetPlayerList()
        {
            var players = _dbSet
                .Include(p => p.TwoTeams)
                .Include(p => p.OneTeams)
                //.Include(p => p.OneTeams.SelectMany(m=>m.Matches))
                .OrderBy(p => p.Score)
                .ThenBy(p => p.AllTimeHigh)
                .ThenBy(p => p.Name)
                .ToList();
            return players;
        }

        public Player GetPlayer(int playerId)
        {
            var player = _dbSet.FirstOrDefault(p => p.Id == playerId);
            return player;
        }


    }
}
