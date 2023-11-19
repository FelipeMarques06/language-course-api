using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Application.Dtos
{
    public class StudentDtoRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
