using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.DomainModel.Model.New;
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
            if(!ModelState.IsValid) return View(vm);
            _fussballRepository.CreatePlayer(new Player { Name = vm.Name, Initials = vm.Initials });
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateMatch()
        {
            var TestModel = new CreateMatchViewModel();

            //TODO Remove test data
            {
                {
                    TestModel.PlayerOne = "MIB";
                    TestModel.PlayerTwo = "BOS";
                    TestModel.PlayerThree = "MRA";
                    TestModel.PlayerFour = "ANJ";
                }
            }
            return View(TestModel);
        }

        [HttpPost]
        public ActionResult CreateMatch(CreateMatchViewModel vm)
        {
            var strList = new List<string> { vm.PlayerOne, vm.PlayerTwo, vm.PlayerThree, vm.PlayerFour };
            var players = _fussballRepository.GetPlayers().ToList();
            var playerToMatch = players.Where(item => strList.Contains(item.Initials)).OrderBy(p => p.Score).ThenBy(i => Guid.NewGuid()).ToList();
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
            Match model;
            if (RedOrBlueTeam())
            {
                model = _fussballRepository.CreateMatch(vm.PlayerOneId, vm.PlayerTwoId, vm.PlayerThreeId, vm.PlayerFourId);
            }
            else
            {
                model = _fussballRepository.CreateMatch(vm.PlayerThreeId, vm.PlayerFourId, vm.PlayerOneId, vm.PlayerTwoId);
            }
            _unitOfWork.Save();
            return RedirectToAction("MatchByGuid", new {id = model.Id});
        }


        [HttpGet]
        public ActionResult MatchByGuid(int id)
        {
            var match = _fussballRepository.GetMatch(id);
            var vm = _mapper.ToViewModel(match);
            return View("Match", vm);
        }

        [HttpPost]
        public ActionResult Match(MatchViewModel vm)
        {
            var match = _fussballRepository.GetMatch(vm.Id);
            if (!match.Done)
            {
                new MatchCon().SetResult(match, vm.EndGoalsTeamRed, vm.EndGoalsTeamBlue);
                _unitOfWork.Save();
            }
            return RedirectToAction("Result", new { id = match.Id });
        }


        public ActionResult Result(int id)
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