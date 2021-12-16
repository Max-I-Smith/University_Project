using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_DATA_EF
{
    public class CourseMetadata
    {
        [Required(ErrorMessage ="**Required**")]
        [StringLength(50, ErrorMessage ="**Must be less than 50 characters**")]
        [Display(Name ="Course")]
        public string CourseName { get; set; }

        [Required(ErrorMessage ="**Required**")]
        [Display(Name = "Course Description")]
        [UIHint("MultilineText")]
        public string CourseDescription { get; set; }

        [Required(ErrorMessage = "**Required**")]
        [Range(1,255, ErrorMessage ="Must be a number 1-255")]
        [Display(Name ="Credit Hours")]
        public byte CreditHours { get; set; }

        [StringLength(250, ErrorMessage ="**Must be less than 250 characters**")]
        [UIHint("MultilineText")]
        public string Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "**Must be less than 500 characters**")]
        [UIHint("MultilineText")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "**Required**")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course
    {

    }
}
