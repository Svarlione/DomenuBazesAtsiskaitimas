using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomenuBazesAtsiskaitimas
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int UnicCode { get; set; }

        // studientas priskiriamas tik prie vieno fakultieto
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        // studentas turi daug paskaitu
        public List<Lecture> Lectures { get; set; } = new List<Lecture>();


    }
}
