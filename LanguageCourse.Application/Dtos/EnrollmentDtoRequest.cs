using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Application.Dtos
{
    public class EnrollmentDtoRequest
    {
        public int StudentId { get; set; }
        public int AcademicClassId { get; set; }
    }
}
