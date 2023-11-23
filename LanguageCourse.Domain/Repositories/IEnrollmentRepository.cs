using LanguageCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourse.Domain.Repositories
{
    public interface IEnrollmentRepository: IRepository<Enrollment>
    {
        public bool IsStudentEnrolled(int studentId, int academicClassId);
        public bool ClassHasEnrolledStudents(int classId);
    }
}
