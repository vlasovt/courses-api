using CourseLibrary.API.Models;
using CourseLibrary.API.ValidationAttributes;

namespace CourseLibrary.API.Model
{
    [CourseTitleMustBeDifferentFromDescription]
    public class CreateCourseDto: CourseManipulationDto
    {
    }
}
