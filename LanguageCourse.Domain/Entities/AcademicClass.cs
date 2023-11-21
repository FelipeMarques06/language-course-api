using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Domain.Entities
{
    public class AcademicClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AcademicYear { get; set; }
        public DateTime Created_at { get; set; }
    }
}
