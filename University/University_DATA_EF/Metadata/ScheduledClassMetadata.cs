using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_DATA_EF
{
    public class ScheduledClassMetadata
    {
        [Required(ErrorMessage ="**Required**")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "**Required**")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "**Required**")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "**Required**")]
        [StringLength(40, ErrorMessage ="**Must be less than 40 characters**")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "**Required**")]
        [StringLength(20, ErrorMessage = "**Must be less than 20 characters**")]
        public string Location { get; set; }

        [Required(ErrorMessage = "**Required**")]
        public int SCSID { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {
        [Display(Name ="Class Info")]
        public string ClassInfo
        {
            get { return Courses.CourseName + " start date " + StartDate + " at " + Location; }
        }
    }
}
