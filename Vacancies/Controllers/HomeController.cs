using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vacancies.DAL;
using Vacancies.DAL.Models;

namespace Vacancies.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public HomeController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index(List<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                vacancies = _databaseContext.Vacancies.ToList();
            return View("Home", vacancies);
        }
    }
}