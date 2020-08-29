using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD1.Models
{
    public class ClassStudentDetails
    {
        public int StudentCourseId { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
    }
}