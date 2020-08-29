using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVC_CRUD1.Models;

namespace MVC_CRUD1.Database_Access_Layer
{
    public class StudentDetailsdb
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);

        public DataSet Show_StudentCourse_All()
        {
            SqlCommand com = new SqlCommand("Sp_SelectAllStudentCourse", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public List<ClassStudentDetails> PopulateStudent()
        {
            List<ClassStudentDetails> DDL_stu = new List<ClassStudentDetails>();
            SqlCommand cmd = new SqlCommand("Sp_PopulateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    DDL_stu.Add(new ClassStudentDetails
                    {
                        StudentId = Convert.ToInt32(sdr["StudentId"]),
                        Name = sdr["Name"].ToString(),                         
                    });
                }
            }
            con.Close();
            return DDL_stu;
        }
        public List<ClassStudentDetails> PopulateCourse()
        {
            List<ClassStudentDetails> DDL_crs = new List<ClassStudentDetails>();
            SqlCommand cmd = new SqlCommand("Sp_PopulateCourse", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    DDL_crs.Add(new ClassStudentDetails
                    {
                        CourseId = Convert.ToInt32(sdr["CourseId"]),
                        Course = sdr["Course"].ToString(),
                    });
                }
            }
            con.Close();
            return DDL_crs;
        }
        public void InsertStudentCourse(ClassStudentDetails s)
        {
            try
            {
                int sid = s.StudentId, cid = s.CourseId;
                bool status = false;
                status = CheckIfExists(sid,cid);

                if (status == false)
                {
                    SqlCommand com = new SqlCommand("Sp_InsertStudentCourse", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@prmsid", s.StudentId);
                    com.Parameters.AddWithValue("@prmcid", s.CourseId);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
                else { }     
            }
            catch (Exception e)
            { Console.WriteLine("Catch error: " + e); }
        }
        public bool CheckIfExists(int sid,int cid)
        {
            SqlCommand cmd = new SqlCommand("CheckIfStudentCourseExists", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prm_sid", sid);
            cmd.Parameters.AddWithValue("@prm_cid", cid);
            con.Open();
            bool status = Convert.ToBoolean(cmd.ExecuteScalar());
            con.Close();
            return status;                                  
        }
        public void DeleteStudentCourse(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("Sp_DeleteStudentCourse", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@prm_scid", id);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            { Console.WriteLine("Catch error: " + e); }
        }
        public DataSet Show_StudentCourse_id(int id)
        {
            SqlCommand com = new SqlCommand("Sp_SelectAStudentCourse", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@prm_scid", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }     
        public void UpdateStudentCourse(ClassStudentDetails s)
        {
            try
            {
                int sid = s.StudentId, cid = s.CourseId;
                bool status = false;
                status = CheckIfExists(sid, cid);

                if (status == false)
                {
                    SqlCommand com = new SqlCommand("Sp_UpdateStudentCourse", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@prmscid", s.StudentCourseId);
                    com.Parameters.AddWithValue("@prmsid", s.StudentId);
                    com.Parameters.AddWithValue("@prmcid", s.CourseId);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
                else { }
            }
            catch (Exception e)
            { Console.WriteLine("Catch error: " + e); }
        }
    }
}