using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomenuBazesAtsiskaitimas
{
    public class Lecture
    {
        [Key]
        public int LectureId { get; set; }
        public string Name { get; set; }
        public int UnicCode { get; set; }

        // Kiekviena paskaita gali but priskirta tik vienam fakultietui

        public List<Faculty> Faculties { get; set; } = new List<Faculty>();

        // paskaitai gali buti priskirta daug studentu
        public List<Student> Students { get; set; } = new List<Student>();

    }
}
