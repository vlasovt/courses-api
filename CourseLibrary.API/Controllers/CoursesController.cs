using AutoMapper;
using CourseLibrary.API.Model;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController: ControllerBase
    {
        private ICourseLibraryRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _courseRepository = repository 
                ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if(!_courseRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courses = _courseRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}")]
        public IActionResult GetCourses(Guid authorId, Guid courseId)
        {
            if (!_courseRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = _courseRepository.GetCourse(authorId, courseId);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(course));
        }
    }
}
