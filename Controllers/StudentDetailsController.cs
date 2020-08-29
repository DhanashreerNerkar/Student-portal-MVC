using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MVC_CRUD1.Models;


namespace MVC_CRUD1.Controllers
{
    public class StudentDetailsController : Controller
    {
        Database_Access_Layer.StudentDetailsdb dblayer = new Database_Access_Layer.StudentDetailsdb();
        // GET: StudentDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentCourseDisplay()
        {
            DataSet ds = dblayer.Show_StudentCourse_All();
            ViewBag.stucors = ds.Tables[0];
            return View();
        }
        public ActionResult StudentCourseInsert()
        {
            ViewBag.DDL_stu = dblayer.PopulateStudent();
            ViewBag.DDL_crs = dblayer.PopulateCourse();
            return View();
        }
        [HttpPost]
        public ActionResult StudentCourseInsert(FormCollection f)
        {
            ClassStudentDetails s = new ClassStudentDetails();
            s.StudentId = Convert.ToInt32(f["StudentId"]);
            s.CourseId = Convert.ToInt32(f["CourseId"]);
            dblayer.InsertStudentCourse(s);
            return RedirectToAction("StudentCourseDisplay");
        }
        public ActionResult StudentCourseDelete(int id)
        {
            dblayer.DeleteStudentCourse(id);
            return RedirectToAction("StudentCourseDisplay");
        }
        public ActionResult StudentCourseUpdate(int id)
        {
            ViewBag.DDL_stu = dblayer.PopulateStudent();
            ViewBag.DDL_crs = dblayer.PopulateCourse();
            DataSet ds = dblayer.Show_StudentCourse_id(id);
            ViewBag.StuCorsRecord = ds.Tables[0];
            return View();
        }
        [HttpPost]
        public ActionResult StudentCourseUpdate(int id, FormCollection f)
        {
            ClassStudentDetails s = new ClassStudentDetails();
            s.StudentCourseId = id;
            s.StudentId = Convert.ToInt32(f["StudentId"]);
            s.CourseId = Convert.ToInt32(f["CourseId"]);
            dblayer.UpdateStudentCourse(s);
            return RedirectToAction("StudentCourseDisplay");
        }
    }
}