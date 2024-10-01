using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Data.Repositories;
using PortfolioAPI.Entities;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ExperienceRepository _experienceRepository;
        public ExperienceController(ExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return Ok(_experienceRepository.Experiences);
            }
            else
            {
                return Ok(_experienceRepository.Experiences.Where(e => e.State == "Active"));
            }
        }

        [HttpGet("{titleForSearch}")]
        public IActionResult Get(string titleForSearch)
        {
            return Ok(_experienceRepository.Experiences.Where(e => e.Title.Contains(titleForSearch)));
        }

        [HttpPost]
        public IActionResult AddExperience([FromBody] ExperienceForCreateAndUpdateRequest requestdto)
        {
            Experience experience = new Experience()
            {
                Id = _experienceRepository.Experiences.Count() + 1,
                Description = requestdto.Description,
                Title = requestdto.Title,
                ImagePath = requestdto.ImagePath,
                Summary = "En proceso"
            };

            _experienceRepository.Experiences.Add(experience);
            return Ok(_experienceRepository.Experiences);
        }

        [HttpPut("{idExperience}")]
        public IActionResult Update([FromRoute]int idExperience, [FromBody] ExperienceForCreateAndUpdateRequest requestDto)
        {
            int idExperienceToModify = _experienceRepository.Experiences.FindIndex(e => e.Id == idExperience);
            if (idExperienceToModify != -1)
            {
                Experience newExperience = new Experience()
                {
                    Id = idExperience,
                    Description = requestDto.Description,
                    Title = requestDto.Title,
                    ImagePath = requestDto.ImagePath,
                    Summary = _experienceRepository.Experiences[idExperienceToModify].Summary
                };
                _experienceRepository.Experiences[idExperienceToModify] = newExperience;
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{idExperience}")]
        public IActionResult Deletee(int idExperience)
        {
            int idExperienceToDelete = _experienceRepository.Experiences.FindIndex(e => e.Id == idExperience);
            if (idExperienceToDelete != -1)
            {
                Experience deletedExperience = new Experience()
                {
                    Id = idExperience,
                    Description = _experienceRepository.Experiences[idExperienceToDelete].Description,
                    Title = _experienceRepository.Experiences[idExperienceToDelete].Title,
                    ImagePath = _experienceRepository.Experiences[idExperienceToDelete].ImagePath,
                    Summary = _experienceRepository.Experiences[idExperienceToDelete].Summary,
                    State = "Deleted"
                };
                _experienceRepository.Experiences[idExperienceToDelete] = deletedExperience;
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }



    }
}
