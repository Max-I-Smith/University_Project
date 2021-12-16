using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_DATA_EF
{
    public class StudentStatusMetadata
    {
        [Required(ErrorMessage ="**Required**")]
        [StringLength(30, ErrorMessage ="**Must be less than 30 characters**")]
        [Display(Name = "Student Status")]
        public string SSName { get; set; }

        [StringLength(250, ErrorMessage = "**Must be less than 250 characters**")]
        [Display(Name = "Status Description")]
        [UIHint("MultilineText")]
        public string SSDescription { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus
    {

    }
}
