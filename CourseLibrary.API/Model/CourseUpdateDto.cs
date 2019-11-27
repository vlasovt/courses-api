using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Models
{
    public class CourseUpdateDto : CourseManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a description.")]
        public override string Description { get => base.Description; set => base.Description = value; }

    }
}
