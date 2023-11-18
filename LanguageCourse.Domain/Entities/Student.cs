using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LanguageCourse.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public Student(string name, string cpf, string email)
        {
            // Validation logic (cpf and email) to add here
            Name = name;
            Cpf = cpf;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
