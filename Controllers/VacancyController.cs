using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vacancies.DAL;
using Vacancies.DAL.Models;
using Vacancies.Services;

namespace Vacancies.Controllers
{
    [Route("[controller]")]
    public class VacanciesController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ParsingService _parsingService;
        private const string Url = "https://kaluga.hh.ru/catalog/Informacionnye-tehnologii-Internet-Telekom/";

        public VacanciesController(DatabaseContext databaseContext, ParsingService parsingService)
        {
            _databaseContext = databaseContext;
            _parsingService = parsingService;
        }

        [HttpGet]
        [Route("update/")]
        public async Task<IActionResult> Update()
        {
            List<Vacancy> toDelete = _databaseContext.Vacancies.ToList();
            _databaseContext.RemoveRange(toDelete);
            _databaseContext.SaveChanges();

            List<Vacancy> vacancies = await _parsingService.GetVacancies(Url);
            await _databaseContext.Vacancies.AddRangeAsync(vacancies);
            await _databaseContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}