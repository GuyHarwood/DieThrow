using System.Web.Mvc;
using GuyHarwood.DieThrow.Domain;
using GuyHarwood.DieThrow.Domain.Core;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHandler<ThrowDice, HighestStreak> _handler;

        public HomeController(IHandler<ThrowDice, HighestStreak> handler)
        {
            _handler = handler;
        }

        public ActionResult Index()
        {
            return View("Index",new HomeIndexModel());
        }

        [HttpPost]
        public ViewResult Index(HomeIndexModel model)
        {
            var result = _handler.Handle(new ThrowDice()
            {
                NumberOfTimes = model.ThrowCount
            });

            model.LargestStreak = string.Format("The largest winning streak (sixes in a row) was {0}", result);

            return View(model);
        }
    }
}