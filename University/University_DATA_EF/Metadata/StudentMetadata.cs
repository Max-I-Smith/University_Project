using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_DATA_EF
{
    public class StudentMetadata
    {
        [Required(ErrorMessage ="**Required**")]
        [StringLength(25, ErrorMessage ="**Must be less than 25 characters")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "**Required**")]
        [StringLength(25, ErrorMessage = "**Must be less than 25 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "**Must be less than 50 characters")]
        public string Major { get; set; }

        [StringLength(50, ErrorMessage = "**Must be less than 50 characters")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "**Must be less than 50 characters")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "**Must be 2 characters")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "**Must be less than 10 characters")]
        [Display(Name ="Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(13, ErrorMessage = "**Must be 13 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "**Required**")]
        [EmailAddress(ErrorMessage ="**Must be a valid email**")]
        [StringLength(60, ErrorMessage = "**Must be less than 60 characters")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "**Must be less than 100 characters")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "**Required**")]
        public int SSID { get; set; }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        [Display(Name ="Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
