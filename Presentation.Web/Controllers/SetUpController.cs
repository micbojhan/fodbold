using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Core.DomainModel.Model;
using Core.DomainModel.Model.New;
using Core.DomainServices;
using Infrastructure.DataAccess.Repositorys;
using Presentation.Web.Mappers;
using Presentation.Web.ViewModels;
using Match = Core.DomainModel.Model.Match;

namespace Presentation.Web.Controllers
{
    public class SetUpController : Controller
    {
        private readonly IFussballRepository _fussballRepository;
        //private readonly IGenericRepository<Derby> _derbyRepository;
        private readonly IGenericRepository<Match> _matcheRepository;
        private readonly IGenericRepository<Team> _teamRepository;
        private readonly IGenericRepository<Player> _playerRepository;
        private readonly IGenericRepository<Test> _testRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ManuelMapper _mapper;

        public SetUpController(
            IFussballRepository fussballRepository, 
            IGenericRepository<Test> testRepository, 
            //IGenericRepository<Derby> derbyRepository, 
            IGenericRepository<Match> matcheRepository, 
            IGenericRepository<Team> teamRepository, 
            IGenericRepository<Player> playerRepository, 
            IUnitOfWork unitOfWork)
        {
            _fussballRepository = fussballRepository;
            //_derbyRepository = derbyRepository;
            _matcheRepository = matcheRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _testRepository = testRepository;
            _unitOfWork = unitOfWork;
            _mapper = new ManuelMapper();

        }
        public ActionResult SetUpDB()
        {
            if (_testRepository.Any()) _testRepository.RemoveRange(_testRepository.AsQueryable());
            //if (_derbyRepository.Any()) _derbyRepository.RemoveRange(_derbyRepository.AsQueryable());
            _unitOfWork.Save();
            if (_matcheRepository.Any()) _matcheRepository.RemoveRange(_matcheRepository.AsQueryable());
            _unitOfWork.Save();
            if (_teamRepository.Any()) _teamRepository.RemoveRange(_teamRepository.AsQueryable());
            if (_playerRepository.Any()) _playerRepository.RemoveRange(_playerRepository.AsQueryable());

            _unitOfWork.Save();



            var players = itMinds();
            //var players = dit();
            _playerRepository.AddRange(players.ToList());
            _unitOfWork.Save();
            itMindsMatch();


            return RedirectToAction("CreateMatch", "MvcMatch");
        }

        private CreateMatchViewModel SetTeam(string et, string to, string tre, string fire)
        {
            var players = _playerRepository.AsQueryable();
            var model = new CreateMatchViewModel();
            model.PlayerOneId = players.First(p => p.Initials == et).Id;
            model.PlayerTwoId = players.First(p => p.Initials == to).Id;
            model.PlayerThreeId = players.First(p => p.Initials == tre).Id;
            model.PlayerFourId = players.First(p => p.Initials == fire).Id;
            var hold1team1 = _fussballRepository.CreateOrGetTeam(model.PlayerOneId, model.PlayerTwoId);
            var hold1team2 = _fussballRepository.CreateOrGetTeam(model.PlayerThreeId, model.PlayerFourId);
            return model;
        }

        private void itMindsMatch()
        {
            var hold1 = SetTeam("ESD", "MIB", "CEN", "RAN");
            Match(hold1 , 10 , 1);
            var hold2 = SetTeam("ESD", "JAG", "RAN", "MIB");
            Match(hold2, 5, 10);
            var hold3 = SetTeam("ESD", "RAN", "HRC", "MIB");
            Match(hold3, 5, 10);
        }

        private void Match(CreateMatchViewModel hold, int s1, int s2)
        {
            _unitOfWork.SaveChanges();
            var match = _fussballRepository.CreateMatch(hold.PlayerOneId, hold.PlayerTwoId, hold.PlayerThreeId, hold.PlayerFourId);
            _unitOfWork.SaveChanges();
            var dbmatch = _fussballRepository.GetMatch(match.Id);
            var dbMatchsaved = new MatchCon().SetResult(dbmatch, s1, s2);
            _unitOfWork.SaveChanges();
        }

        private List<Player> itMinds()
        {
            List<Player> players = new List<Player>
            {
                new Player {Initials = "MIB", Name = "Michael BH"},
                new Player {Initials = "ESD", Name = "Eskild D"},
                new Player {Initials = "CEN", Name = "Cecilia E"},
                new Player {Initials = "RAN", Name = "Rasmus AN"},
                new Player {Initials = "JAG", Name = "Jimmi A"},
                new Player {Initials = "HRC", Name = "Henning RC"},
            };
            return players;
        }
        private List<Player> dit()
        {
            List<Player> players = new List<Player>
            {
                new Player {Initials = "KIH", Name = "Kasper IH", Score = -8, AllTimeHigh = -8, AllTimeLow = -8},
                new Player {Initials = "MMJ", Name = "Morten M", Score = -6, AllTimeHigh = -6, AllTimeLow = -6},
                new Player {Initials = "CDO", Name = "Caspar", Score = -5, AllTimeHigh = -5, AllTimeLow = -5},
                new Player {Initials = "EDK", Name = "Ebbe DK", Score = -3, AllTimeHigh = -3, AllTimeLow = -3},
                new Player {Initials = "KAK", Name = "Kamayr", Score = -1, AllTimeHigh = -1, AllTimeLow = -1},
                new Player {Initials = "POK", Name = "Poul K", Score = 0, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "MLY", Name = "Morten L", Score = 1, AllTimeHigh = 1, AllTimeLow = 1},
                new Player {Initials = "OVD", Name = "Ole VD", Score = 2, AllTimeHigh = 2, AllTimeLow = 2},
                new Player {Initials = "KRF", Name = "Kristian FØ", Score = 2, AllTimeHigh = 2, AllTimeLow = 2},
                new Player {Initials = "PWK", Name = "Peter WK", Score = 3, AllTimeHigh = 3, AllTimeLow = 3},
                new Player {Initials = "BOS", Name = "Bo S", Score = 3, AllTimeHigh = 3, AllTimeLow = 3},
                new Player {Initials = "SEL", Name = "Sebastian", Score = 5, AllTimeHigh = 0, AllTimeLow = 5},
                new Player {Initials = "MOD", Name = "Morten D", Score = 6, AllTimeHigh = 3, AllTimeLow = 6},
                new Player {Initials = "TLU", Name = "Tore", Score = 8, AllTimeHigh = 8, AllTimeLow = 8},
                new Player {Initials = "KHA", Name = "Kim Hansen", Score = 9, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "LCH", Name = "Lars Anker", Score = 10, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "BOH", Name = "Bo Hansen", Score = 11, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "JSB", Name = "Jan Jønson", Score = 14, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "RES", Name = "Rasmus", Score = 15, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "MBH", Name = "Michael BH", Score = 17, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "KST", Name = "Kennet", Score = 17, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "JHE", Name = "Jens", Score = 17, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "JOL", Name = "Jonas L", Score = 18, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "MRA", Name = "Morten R", Score = 18, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "MIB", Name = "Mikkel Bj", Score = 18, AllTimeHigh = 0, AllTimeLow = 0},
                new Player {Initials = "MEN", Name = "Mikkel Bisp", Score = 19, AllTimeHigh = 19, AllTimeLow = 19},
            };
            return players;
        }

        public ActionResult RandomMatch(int id)
        {
            int mId = 0;
            for (int j = 0; j < id; j++)
            {
                var playerToMatch = PlayerToMatch();

                var model = new MatchCon().CreateMatchViewModel(new CreateMatchViewModel(), playerToMatch);
                var teamone = _fussballRepository.CreateOrGetTeam(model.PlayerOneId, model.PlayerTwoId);
                var teamtwo = _fussballRepository.CreateOrGetTeam(model.PlayerThreeId, model.PlayerFourId);
                _unitOfWork.SaveChanges();

                Match matchtemp;
                Random random = new Random();
                int randomNr = random.Next(0, 2); // creates a number between 0 and 51
                if (randomNr == 0)
                {
                    matchtemp = _fussballRepository.CreateMatch(model.PlayerOneId, model.PlayerTwoId, model.PlayerThreeId, model.PlayerFourId);
                }
                else
                {
                    matchtemp = _fussballRepository.CreateMatch(model.PlayerThreeId, model.PlayerFourId, model.PlayerOneId, model.PlayerTwoId);
                }

                _unitOfWork.SaveChanges();
                var match = _fussballRepository.GetMatch(matchtemp.Id);
                var dbMatch = new MatchCon().SetResult(match, matchtemp.EndGoalsTeamRed + random.Next(1, 11), matchtemp.EndGoalsTeamBlue + random.Next(1, 11));
                _unitOfWork.SaveChanges();
                mId = match.Id;
            }
            return RedirectToAction("Result", "MvcMatch", new { id = mId });
        }

        private IOrderedEnumerable<Player> PlayerToMatch()
        {
            Random random = new Random();
            var players = _fussballRepository.GetPlayerList().OrderBy(p => p.Id).ToList();
            var first = players.FirstOrDefault().Id;
            var last = players.LastOrDefault().Id;
            return PlayerToMatch(random, players, first, last);
        }

        private IOrderedEnumerable<Player> PlayerToMatch(Random random, List<Player> players, int first, int last)
        {
            int randomNr1 = random.Next(first, last + 1);
            int randomNr2 = random.Next(first, last + 1);
            int randomNr3 = random.Next(first, last + 1);
            int randomNr4 = random.Next(first, last + 1);
            var strList = new List<int> { randomNr1, randomNr2, randomNr3, randomNr4 };
            var playerToMatch = players.Where(item => strList.Contains(item.Id)).OrderBy(p => p.Score);
            if (playerToMatch.Distinct().Count() != 4) // Fail first
                playerToMatch = PlayerToMatch(random, players, first, last);
            return playerToMatch;
        }
    }
}