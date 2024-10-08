
using PortfolioAPI.Entities;

namespace PortfolioAPI.Data.Repositories
{
    public class ExperienceRepository
    {
        private readonly ApplicationContext _context;
        public ExperienceRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Experience> GetAllExperience()
        {
            return _context.Experiences.ToList();
        }

        public int Add(Experience experience)
        {
            _context.Experiences.Add(experience);
            _context.SaveChanges();
            return experience.Id;

        }

    }
}
