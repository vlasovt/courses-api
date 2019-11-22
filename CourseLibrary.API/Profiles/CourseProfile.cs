using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Model;

namespace CourseLibrary.API.Profiles
{
    public class CourseProfile: Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<CreateCourseDto, Course>();
        }
    }
}
