using Microsoft.EntityFrameworkCore;
using Vacancies.DAL.Models;

namespace Vacancies.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        public DbSet<Vacancy> Vacancies { get; set; }
    }
}