using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace DomenuBazesAtsiskaitimas
{
    public class Program
    {
        public bool IsStudentExists(string studentName)//patikrina ar toks studentas jau egzestoja domenu bazeje
        {
            using var dbContext = new UnuversityContext();


            return dbContext.Students.Any(s => s.FullName == studentName);
        }

        public bool IsFacultyExists(string facultyName) //patikrina ar toks fakultetas jau egzestoja domenu bazeje
        {
            using var dbContext = new UnuversityContext();

            return dbContext.Faculties.Any(f => f.Name == facultyName);

        }

        public bool IsLectureExists(string lectureName) //patikrina ar tokia paskaita jau egzestoja domenu bazeje
        {
            var dbContext = new UnuversityContext();


            return dbContext.Lectures.Any(l => l.Name == lectureName);
        }

        private bool IsLettersOnly(string input)//patikra raidziu
        {
            return input.All(char.IsLetter);
        }

        public string RandomString()// fakultieto unikalaus kodo generacija
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] randomArray = new char[6];
            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomArray);
        }

        public int RandomInt()//studento ir paskaitos(pagal uzduoti nereikalingas) unikalaus kodo generacija
        {
            const string digits = "0123456789";
            StringBuilder randomString = new StringBuilder(8);
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                randomString.Append(digits[random.Next(digits.Length)]);
            }

            return int.Parse(randomString.ToString());
        }

        public void GreatFaculty()//kuriam nauja Fakultieta, 2-ju vienodu fakultietu negalima sukurti
        {
            using var dbContext = new UnuversityContext();

            Console.WriteLine("Irasykite naujo Fakultieto pavadinima, Aciu");
            string facultyName = Console.ReadLine();

            var newFaculty = new Faculty
            {
                Name = facultyName,
                UnicCode = RandomString(),
            };
            if (!IsFacultyExists(facultyName))
            {

                dbContext.Faculties.Add(newFaculty);
                dbContext.SaveChanges();

                Console.WriteLine($"Naujas fakultietas sukurtas: {newFaculty.Name}, Unikalus kodas: {newFaculty.UnicCode}");
            }
            else
            {
                Console.WriteLine("Klaida: Fakultietas su tokiu pavadinimu jau egzistuoja.");
            }

        }

        public void GreatLecture()//kuriam nauja Lekcija, negalima sukurti tokios pat lekcijos
        {
            using var dbContext = new UnuversityContext();

            Console.WriteLine("Irasykite Paskaitos pavadinima, Aciu");
            string lectureName = Console.ReadLine();
            if (IsLettersOnly(lectureName))
            {
                var newLecture = new Lecture
                {
                    Name = lectureName,
                    UnicCode = RandomInt(),
                };

                if (!IsLectureExists(lectureName))
                {

                    dbContext.Lectures.Add(newLecture);
                    dbContext.SaveChanges();

                    Console.WriteLine($"Nauja paskaita sukurta: {newLecture.Name}, Unikalus kodas: {newLecture.UnicCode}");
                }
                else
                {
                    Console.WriteLine("Klaida: Paskaita su tokiu pavadinimu jau egzistuoja.");
                }
            }
            else
            {
                Console.WriteLine("Klaida: Paskaita gali tureti tik raides.");
            }
        }

        public void GreatStudent()// kuriam nauja Studenta su valifdacija kad V.P gali buti tik raides,ir studentas negali pasikartoti
        {
            using var dbContext = new UnuversityContext();

            Console.WriteLine("Irasykite naujo Studento Varda,Pavarde. Aciu");
            string studentName = Console.ReadLine();

            if (IsLettersOnly(studentName))
            {
                var newStudent = new Student
                {
                    FullName = studentName,
                    UnicCode = RandomInt(),
                };

                if (!IsStudentExists(studentName))
                {
                    dbContext.Students.Add(newStudent);
                    dbContext.SaveChanges();

                    Console.WriteLine($"Naujas studentas sukurtas: {newStudent.FullName}, Unikalus kodas: {newStudent.UnicCode}");
                }
                else
                {
                    Console.WriteLine("Klaida: Studentas su tokiu vardu ir pavardė jau egzistuoja.");
                }
            }
            else
            {
                Console.WriteLine("Klaida: Vardas ir Pavarde gali tureti tik raides.");
            }
        }

        public void AddStudentToFaculty()//pridedam studenta prie fakultieto su patikrinimais, ir priskiariam jam to fakultieto paskaitas(su salyga kad jis nera tame fakultiete)
        {
            using var dbContext = new UnuversityContext();

            // Isvedam fakultietu list
            Console.WriteLine("Pasirinkit Fakultieta:");
            var faculties = dbContext.Faculties.ToList();
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"{faculty.FacultyId}. {faculty.Name}");
            }

            // Nuskaitom ivesti
            Console.Write("Iveskite Fakultieto ID: ");
            if (int.TryParse(Console.ReadLine(), out var selectedFacultyId))
            {
                // Gaunam fakultieta
                var selectedFaculty = faculties.FirstOrDefault(f => f.FacultyId == selectedFacultyId);

                if (selectedFaculty != null)
                {
                    // isvedam studentu list
                    Console.WriteLine("Iveskite studento ID:");
                    if (int.TryParse(Console.ReadLine(), out var studentId))
                    {
                        // tikrinam ar yra studentas
                        var existingStudent = dbContext.Students.Include(s => s.Lectures).FirstOrDefault(s => s.StudentId == studentId);

                        if (existingStudent != null)
                        {
                            // tikrinam ar studento nera sarase fakultieto
                            if (!selectedFaculty.Students.Any(s => s.StudentId == existingStudent.StudentId))
                            {
                                // gaunam visas fakultieto paskaitas
                                var facultyLectures = selectedFaculty.Lectures.ToList();

                                // pridiedam paskaitas prie studento
                                existingStudent.Lectures.AddRange(facultyLectures);


                                selectedFaculty.Students.Add(existingStudent);


                                dbContext.SaveChanges();
                                Console.WriteLine($"Studentas {existingStudent.FullName} sekmingai priskirtas prie Fakultieto {selectedFaculty.Name}.");
                            }
                            else
                            {
                                Console.WriteLine($"Studentas {existingStudent.FullName} jau yra priskitras prie Fakultieto {selectedFaculty.Name}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Studentas su tokiu ID {studentId} neegzestoja domenu bazeje.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nekorektiska ivestis studento ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Tokio Fakultieto nera.");
                }
            }
            else
            {
                Console.WriteLine("Nekoreksta ivestis Fakultiete.");
            }
        }

        public void AddLecturesToFaculty()//pridedam studenta prie fakultieto su patikrinimais
        {
            using var dbContext = new UnuversityContext();


            Console.WriteLine("Pasirinkit fakultieta:");
            var faculties = dbContext.Faculties.ToList();
            foreach (var faculty in faculties)
            {

                Console.WriteLine($"{faculty.FacultyId}. {faculty.Name}");
            }


            Console.Write("Iveskite fakultieto numeri: ");
            if (int.TryParse(Console.ReadLine(), out var selectedFacultyId))
            {

                var selectedFaculty = faculties.FirstOrDefault(f => f.FacultyId == selectedFacultyId);

                if (selectedFaculty != null)
                {

                    Console.WriteLine("Pasirinkit Lekcija is saraso:");
                    var lectures = dbContext.Lectures.ToList();
                    foreach (var lecture in lectures)
                    {
                        Console.WriteLine($"{lecture.LectureId}. {lecture.Name}");
                    }


                    Console.Write("Iveskite lekcijos numeri: ");
                    if (int.TryParse(Console.ReadLine(), out var selectedLectureId))
                    {

                        var selectedLecture = lectures.FirstOrDefault(l => l.LectureId == selectedLectureId);

                        if (selectedLecture != null)
                        {

                            selectedFaculty.Lectures.Add(selectedLecture);


                            dbContext.SaveChanges();

                            Console.WriteLine($"Lekcija {selectedLecture.Name} sekmingai pridieta prie fakultieto {selectedFaculty.Name}.");
                        }
                        else
                        {
                            Console.WriteLine("Tokios Lekcijos nera.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nekorektiska ivestis.");
                    }
                }
                else
                {
                    Console.WriteLine("Tokio Fakultieto nera.");
                }
            }
            else
            {
                Console.WriteLine("Nekorektiska ivestis.");
            }
        }

        public void PrintStudentsOfFaculty()
        {
            using var dbContext = new UnuversityContext();


            Console.WriteLine("Praso pasirinkit fakultieta:");
            var faculties = dbContext.Faculties.ToList();
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"{faculty.FacultyId}. {faculty.Name}");
            }


            Console.Write("Irasykite Fakultieto ID: ");
            if (int.TryParse(Console.ReadLine(), out var selectedFacultyId))
            {

                var selectedFaculty = faculties.FirstOrDefault(f => f.FacultyId == selectedFacultyId);

                if (selectedFaculty != null)
                {

                    Console.WriteLine($"Fakultieto studentai {selectedFaculty.Name}:");
                    foreach (var student in selectedFaculty.Students)
                    {
                        Console.WriteLine($"{student.StudentId}. {student.FullName}");
                    }
                }
                else
                {
                    Console.WriteLine("Tokio Fakultieto nera.");
                }
            }
            else
            {
                Console.WriteLine("Nekorektiska ivestis fakultiete.");
            }
        }

        public void PrintLecturesOfFaculty()
        {
            using var dbContext = new UnuversityContext();


            Console.WriteLine("Pasirinkit fakultieta:");
            var faculties = dbContext.Faculties.ToList();
            foreach (var faculty in faculties)
            {
                Console.WriteLine($"{faculty.FacultyId}. {faculty.Name}");
            }


            Console.Write("Iveskite Fakultieto ID: ");
            if (int.TryParse(Console.ReadLine(), out var selectedFacultyId))
            {

                var selectedFaculty = faculties.FirstOrDefault(f => f.FacultyId == selectedFacultyId);

                if (selectedFaculty != null)
                {

                    Console.WriteLine($"Fakultieto paskaitos {selectedFaculty.Name}:");
                    foreach (var lecture in selectedFaculty.Lectures)
                    {
                        Console.WriteLine($"{lecture.LectureId}. {lecture.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Tokio Fakultieto nera.");
                }
            }
            else
            {
                Console.WriteLine("Nekorektiska ivestis fakultiete.");
            }
        }

        public void PrintLecturesOfStudent()
        {
            using var dbContext = new UnuversityContext();


            Console.WriteLine("Pasirinkit studenta:");
            var students = dbContext.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId}. {student.FullName}");
            }


            Console.Write("Iveskite studento ID: ");
            if (int.TryParse(Console.ReadLine(), out var selectedStudentId))
            {

                var selectedStudent = students.FirstOrDefault(s => s.StudentId == selectedStudentId);

                if (selectedStudent != null)
                {

                    Console.WriteLine($"Studento  {selectedStudent.FullName} paskaitos:");
                    foreach (var lecture in selectedStudent.Lectures)
                    {
                        Console.WriteLine($"{lecture.LectureId}. {lecture.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Tokio studento nera.");
                }
            }
            else
            {
                Console.WriteLine("Nekorektiska ivestis studento ID.");
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("hello");
        }
    }
}