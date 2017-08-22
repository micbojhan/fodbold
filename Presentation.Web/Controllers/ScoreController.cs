using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.DomainServices;
using Presentation.Web.Mappers;
using Presentation.Web.ViewModels;

namespace Presentation.Web.Controllers
{
    public class ScoreController : Controller
    {
        private readonly IFussballRepository _fussballRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ManuelMapper _mapper;


        public ScoreController(IFussballRepository fussballRepository, IUnitOfWork unitOfWork)
        {
            _fussballRepository = fussballRepository;
            _unitOfWork = unitOfWork;
            _mapper = new ManuelMapper();

        }

        // GET: Score
        [HttpGet]
        public ActionResult PlayerList()
        {
            //TODO to viewmodel
            var players = _fussballRepository.GetPlayerList().ToList();
            var playersVm = players.Select(p => _mapper.ToViewModel(p)).ToList();
            return View(playersVm);
        }

        [HttpGet]
        public ActionResult Player(int id)
        {
            //TODO to viewmodel
            var player = _fussballRepository.GetPlayer(id);
            var playerVm = _mapper.ToViewModel(player, 3, 15);
            return View(playerVm);
        }

        [HttpGet]
        public ActionResult Team(int id)
        {
            //TODO to viewmodel
            var team = _fussballRepository.GetTeam(id);
            var teamVm = _mapper.ToViewModel(team);
            return View(teamVm);
        }

        [HttpGet]
        public ActionResult TeamList()
        {
            //TODO to viewmodel
            var teams = _fussballRepository.GetTeamList();
            var teamsVm = teams.Select(p => _mapper.ToViewModel(p)).ToList();
            return View(teamsVm);
        }

        [HttpGet]
        public ActionResult MatchList()
        {
            //TODO to viewmodel
            var matches = _fussballRepository.GetMatchList();
            var teamsVm = matches.Select(p => _mapper.ToViewModel(p)).Take(50).ToList();
            return View(teamsVm);
        }
    }
}