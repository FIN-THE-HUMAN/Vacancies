using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Vacancies.DAL.Models;

namespace Vacancies.Services
{
    public class ParsingService
    {
        private const int VacanciesLimit = 50;
        private const int IndexLimit = 3;
        
        
        private async Task<List<Vacancy>> Parse(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);

            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            var parser = new HtmlParser();
            var document = await parser.ParseAsync(stream);
            var vacancies = document.QuerySelectorAll("div.vacancy-serp-item");

            List<Vacancy> vacanciesList = new List<Vacancy>();
            foreach (var item in vacancies)
            {
                Vacancy vacancy = new Vacancy();
                var title = item.QuerySelector("div.search-item-name");
                var company =
                    item.QuerySelector("div.vacancy-serp-item__meta-info>a[data-qa='vacancy-serp__vacancy-employer']");
                var description = item.QuerySelector("div[data-qa='vacancy-serp__vacancy_snippet_responsibility']");
                var requirements = item.QuerySelector("div[data-qa='vacancy-serp__vacancy_snippet_requirement']");

                var salary = item.QuerySelector("div.vacancy-serp-item__compensation");

                if (title != null)
                {
                    vacancy.Title = title.TextContent;
                }

                if (company != null)
                {
                    vacancy.Company = company.TextContent;
                }

                if (description != null)
                {
                    vacancy.Description = description.TextContent;
                }

                if (requirements != null)
                {
                    vacancy.Requirements = requirements.TextContent;
                }

                if (salary != null)
                {
                    vacancy.Salary = salary.TextContent;
                }
                vacanciesList.Add(vacancy);
            }
            return vacanciesList;
        }


        public async Task<List<Vacancy>> GetVacancies(string url)
        {
            int index = 1;
            List<Vacancy> vacancies = new List<Vacancy>();
            vacancies.AddRange(await Parse(url));
            while (vacancies.Count < VacanciesLimit && index <= IndexLimit)
            {
                vacancies.AddRange(await Parse(url + $"page-{index}/"));
                index++;
            }
            return vacancies;
        }
    }
}