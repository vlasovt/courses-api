using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Model;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private ICourseLibraryRepository _courseRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _courseRepository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _courseRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

            return Ok(authorsToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection (IEnumerable<CreateAuthorDto> createAuthorDtos)
        {
            var authors = _mapper.Map<IEnumerable<Author>>(createAuthorDtos);

            foreach(var author in authors)
            {
                _courseRepository.AddAuthor(author);
            }

            _courseRepository.Save();

            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            var idsAsString = string.Join(",", authorDtos.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorDtos);
        }
    }
}
