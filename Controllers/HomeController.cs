using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MVC_CRUD1.Models;
//using PagedList;
//using PagedList.Mvc;

namespace MVC_CRUD1.Controllers
{
    public class HomeController : Controller
    {
        Database_Access_Layer.db dblayer = new Database_Access_Layer.db();
        // GET: Home

        public ActionResult Index()
        { return View(); }
        public ActionResult StudentDisplay(String SearchBy, String search)
        {
            //int pageSize = 7;
            //int pageNumber = 1;
            //pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            if (SearchBy == null || search == null){
                DataSet ds = dblayer.Show_Student_All();               
                ViewBag.stu = ds.Tables[0];
                //ViewBag.stu.ToPagedList(pageNumber, pageSize);
                
            }
            else if (SearchBy != "" && search != ""){
                //page = 1;
                if (SearchBy=="Name"){
                    DataSet ds = dblayer.Show_Student_ByName(search);
                    ViewBag.stu = ds.Tables[0];
                    //ViewBag.stu.ToPagedList(pageNumber, pageSize);
                }
                else if (SearchBy == "Gender"){
                    DataSet ds = dblayer.Show_Student_ByGender(search);
                    ViewBag.stu = ds.Tables[0];
                    //ViewBag.stu.ToPagedList(pageNumber, pageSize);
                }
                else { }                  
            }
            else { }           
            return View();          
        }
        public ActionResult StudentInsert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentInsert(FormCollection f)
        {
            ClassStudent s = new ClassStudent();
            s.Name = f["Name"];
            s.DateOfBirth = f["DateOfBirth"];
            s.Gender = f["Gender"];
            dblayer.InsertStudent(s);
            return RedirectToAction("StudentDisplay");
        }
        public ActionResult StudentUpdate(int id)
        {
            DataSet ds = dblayer.Show_Student_id(id);
            ViewBag.StuRecord = ds.Tables[0];
            return View();
        }
        [HttpPost]
        public ActionResult StudentUpdate(int id,FormCollection f)
        {
            ClassStudent s = new ClassStudent();
            s.StudentID = id;
            s.Name = f["Name"];
            s.DateOfBirth = f["DateOfBirth"];
            s.Gender = f["Gender"];
            dblayer.UpdateStudent(s);
            return RedirectToAction("StudentDisplay") ;
        }       
        [HttpPost]
        public ActionResult StudentDelete(int id)
        {      
            dblayer.DeleteStudent(id);
            return RedirectToAction("StudentDisplay");
        }
    }
}