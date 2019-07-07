using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Vacancies.DAL.Models
{
    public class Vacancy
    {
        [Key] public Guid Id { get; set; }
        public string Title { get; set; }
        public string Salary { get; set; }
        public string Company { get; set; }
        public string Requirements { get; set; }
        public string Description { get; set; }
    }
}