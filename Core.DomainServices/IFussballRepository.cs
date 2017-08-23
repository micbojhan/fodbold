using System.Collections.Generic;
using Core.DomainModel.Model.New;

namespace Core.DomainServices
{
    public interface IFussballRepository
    {
        List<Player> GetPlayers();
        List<Player> GetPlayerList();
        List<Team> GetTeamList();
        List<Match> GetMatchList();
        Player GetPlayer(int playerId);
        Team GetTeam(int teamId);
        Player CreatePlayer(Player p);
        Team CreateOrGetTeam(int playerOneId, int playerTwoId);
        Team GetTeam(int playerOneId, int playerTwoId);
        Match CreateMatch(int playerOneId, int playerTwoId, int playerThreeId, int playerFourId);
        Match GetMatch(int id);
    }
}
