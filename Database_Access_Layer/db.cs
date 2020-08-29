using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MVC_CRUD1.Models;

namespace MVC_CRUD1.Database_Access_Layer
{
    public class db
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);

        public void InsertStudent(ClassStudent s)
        {
            try
            {
                SqlCommand com = new SqlCommand("Sp_InsertStudent", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@prm_snam", s.Name);
                com.Parameters.AddWithValue("@prm_sdob", s.DateOfBirth);
                com.Parameters.AddWithValue("@prm_sgen", s.Gender);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            { Console.WriteLine("Catch error: "+e); }
        }
        public void UpdateStudent(ClassStudent s)
        {
            try
            {
                SqlCommand com = new SqlCommand("Sp_UpdateStudent", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@prm_sid", s.StudentID);
                com.Parameters.AddWithValue("@prm_snam", s.Name);
                com.Parameters.AddWithValue("@prm_sdob", s.DateOfBirth);
                com.Parameters.AddWithValue("@prm_sgen", s.Gender);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            { Console.WriteLine("Catch error: " + e); }
        }
        public void DeleteStudent(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("Sp_DeleteStudent", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@prm_sid", id);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            { Console.WriteLine("Catch error: " + e); }
        }
        public DataSet Show_Student_All()
        {
            SqlCommand com = new SqlCommand("Sp_SelectAllStudent", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }           
        public DataSet Show_Student_id(int id)
        {
            SqlCommand com = new SqlCommand("Sp_SelectAStudent", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@prm_sid",id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet Show_Student_ByName(String search)
        {
            SqlCommand com = new SqlCommand("Sp_SelectStudentByName", con);
            com.CommandType = CommandType.StoredProcedure;   
            com.Parameters.AddWithValue("@prm_srch", search);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet Show_Student_ByGender(String search)
        {
            SqlCommand com = new SqlCommand("Sp_SelectStudentByGender", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@prm_srch", search);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}