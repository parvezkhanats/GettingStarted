using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GettingStarted.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Full Name")]
        [MaxLength(50,ErrorMessage ="Name is Required Field & Less than 50 Char")]
        public string Name { get; set; }
        [Required]
        public DateTime Enrolled { get; set; }
        public ICollection<StudentCourse> Enrollment { get; set; } = new HashSet<StudentCourse>();
    }
}
