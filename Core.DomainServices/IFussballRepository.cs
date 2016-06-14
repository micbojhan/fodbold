using System;
using System.Collections.Generic;
using Core.DomainModel.Model;

namespace Core.DomainServices
{
    public interface IFussballRepository
    {
        List<Player> GetPlayerList();
        List<Team> GetTeamList();
        List<Match> GetMatchList();
        Player GetPlayer(int playerId);
        Player CreatePlayer(Player p);
        Team CreateOrGetTeam(int playerOneId, int playerTwoId);
        Team GetTeam(int playerOneId, int playerTwoId);
        Match CreateMatch(int playerOneId, int playerTwoId, int playerThreeId, int playerFourId);
        Match GetMatch(Guid matchGuid);
    }
}
