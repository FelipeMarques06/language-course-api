﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcademicClassId { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public Student Student { get; set; }
        public AcademicClass AcademicClass { get; set; }
    }
}
