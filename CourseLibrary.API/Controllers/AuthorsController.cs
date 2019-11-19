using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController: ControllerBase
    {
        private ICourseLibraryRepository _courseRepository;

        public AuthorsController(ICourseLibraryRepository repository)
        {
            _courseRepository = repository 
                ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _courseRepository.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthors(Guid authorId)
        {
            var author = _courseRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
    }
}
