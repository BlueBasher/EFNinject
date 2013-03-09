using System.Web.Mvc;
using EFNinjectApplication.CrossCutting.Interfaces;

namespace EFNinjectApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var customers = _unitOfWork.Customers.Fetch();

            var tmp = _unitOfWork.Customers.Fetch();

            return View(customers);
        }
    }
}
