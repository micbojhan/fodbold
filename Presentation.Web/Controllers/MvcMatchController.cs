using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.DomainModel.Model;
using Core.DomainServices;
using Presentation.Web.Mappers;
using Presentation.Web.ViewModels;

namespace Presentation.Web.Controllers
{
    public class MvcMatchController : Controller
    {
        private readonly IFussballRepository _fussballRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ManuelMapper _mapper;


        public MvcMatchController(IFussballRepository fussballRepository, IUnitOfWork unitOfWork)
        {
            _fussballRepository = fussballRepository;
            _unitOfWork = unitOfWork;
            _mapper = new ManuelMapper();

        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreatePlayer()
        {
            return View(new CreatePlayerViewModel());
        }

        [HttpPost]
        public ActionResult CreatePlayer(CreatePlayerViewModel vm)
        {
            //TODO validate vm
            _fussballRepository.CreatePlayer(new Player { Name = vm.Name, Initials = vm.Initials, Score = vm.Score });
            _unitOfWork.Save();
            return View(vm);
        }

        [HttpGet]
        public ActionResult CreateMatch()
        {
            var TestModel = new CreateMatchViewModel();

            //TODO Remove test data
            {
                {
                    TestModel.PlayerOne = "MIB";
                    TestModel.PlayerTwo = "ESD";
                    TestModel.PlayerThree = "RAN";
                    TestModel.PlayerFour = "JAG";
                }
            }

            return View(TestModel);
        }

        [HttpPost]
        public ActionResult CreateMatch(CreateMatchViewModel vm)
        {
            var strList = new List<string> { vm.PlayerOne, vm.PlayerTwo, vm.PlayerThree, vm.PlayerFour };

            var players = _fussballRepository.GetPlayerList();
            var playerToMatch = players.Where(item => strList.Contains(item.Initials)).OrderBy(p => p.Score);

            if (playerToMatch.Distinct().Count() != 4) // Fail first
                return View(vm);

            var model = new MatchCon().CreateMatchViewModel(vm, playerToMatch);
            _fussballRepository.CreateOrGetTeam(model.PlayerOneId, model.PlayerTwoId);
            _fussballRepository.CreateOrGetTeam(model.PlayerThreeId, model.PlayerFourId);
            _unitOfWork.Save();

            return RedirectToAction("Match", model);
        }



        [HttpGet]
        public ActionResult Match(CreateMatchViewModel vm)
        {
            MatchViewModel model;
            if (RedOrBlueTeam())
            {
                model = _mapper.ToViewModel(_fussballRepository.CreateMatch(vm.PlayerOneId, vm.PlayerTwoId, vm.PlayerThreeId, vm.PlayerFourId));
            }
            else
            {
                model = _mapper.ToViewModel(_fussballRepository.CreateMatch(vm.PlayerThreeId, vm.PlayerFourId, vm.PlayerOneId, vm.PlayerTwoId));
            }
            _unitOfWork.Save();
            return View(model);
        }


        [HttpGet]
        public ActionResult MatchByGuid(Guid id)
        {
            var match = _fussballRepository.GetMatch(id);
            var vm = _mapper.ToViewModel(match);
            return View("Match", vm);
        }

        [HttpPost]
        public ActionResult Match(MatchViewModel vm)
        {
            var match = _fussballRepository.GetMatch(vm.MatchGuid);
            if (!match.Done)
            {
                new MatchCon().SetResult(match, vm.EndGoalsTeamRed, vm.EndGoalsTeamBlue);
                _unitOfWork.Save();
            }
            return RedirectToAction("Result", new { id = match.MatchGuid });
        }


        public ActionResult Result(Guid id)
        {
            var match = _fussballRepository.GetMatch(id);
            var vm = _mapper.ToViewModel(match);
            return View(vm);
        }



































        private bool RedOrBlueTeam()
        {
            Random random = new Random();
            int randomNr = random.Next(0, 2);
            return randomNr == 0;
        }
    }
}