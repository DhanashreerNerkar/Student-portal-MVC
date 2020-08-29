using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD1.Models
{
    public class ClassStudent
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }        
    }
}