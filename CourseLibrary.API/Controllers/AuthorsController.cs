using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Model;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private ICourseLibraryRepository _courseRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _courseRepository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters resourceParameters)
        {
            var authors = _courseRepository.GetAuthors(resourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId}", Name = "GetAuthors")]
        public IActionResult GetAuthors(Guid authorId)
        {
            var author = _courseRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor (CreateAuthorDto createAuthorDto)
        {
            var author = _mapper.Map<Author>(createAuthorDto);
            _courseRepository.AddAuthor(author);
            _courseRepository.Save();
            var authorDto = _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute("GetAuthors", new { authorId = author.Id}, authorDto);
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
