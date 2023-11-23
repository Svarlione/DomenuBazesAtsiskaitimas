using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomenuBazesAtsiskaitimas
{
    public class Faculty
    {
        [Key]
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string UnicCode { get; set; }

        // fakultetas turi daug studentu
        public List<Student> Students { get; set; }

        // fakultietas gali tureiti daug paskaitu
        public List<Lecture> Lectures { get; set; } = new List<Lecture>();

    }
}
