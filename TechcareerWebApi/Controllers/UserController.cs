using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechcareerWebApi.Models;

namespace TechcareerWebApi.Controllers
{
    public class UserController : ApiController
    {
        UserEntity users = new UserEntity();
        SqlConnection con = new SqlConnection(@"server=localhost\SQLEXPRESS;database=techcareerDB;integrated security = true;");

        // GET api/<controller>
        [HttpGet]
        public List<UserEntity> GetUser()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<UserEntity> lstUser = new List<UserEntity>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserEntity user = new UserEntity();
                    user.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    user.Name = dt.Rows[i]["Name"].ToString();
                    user.Surname = dt.Rows[i]["Surname"].ToString();
                    user.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                    user.Gender = Convert.ToBoolean(dt.Rows[i]["Gender"]);
                    lstUser.Add(user);
                }
            }
            if (lstUser.Count > 0)
                return lstUser;
            else
                return null;
        }

        [HttpGet]
        // GET api/<controller>/5
        public UserEntity Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users WHERE UserID = @id", con);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            UserEntity user = new UserEntity();
            if (dt.Rows.Count > 0)
            {
                user.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                user.Name = dt.Rows[0]["Name"].ToString();
                user.Surname = dt.Rows[0]["Surname"].ToString();
                user.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                user.Gender = Convert.ToBoolean(dt.Rows[0]["Gender"]);
            }
            if (user != null)
                return user;
            else
                return null;
        }

        [HttpPost]
        // POST api/<controller>
        public string Post(UserEntity u)
        {
            string msg = "";
            if (u != null)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (Name, Surname,Age,Gender) VALUES (@Name, @Surname,@Age,@Gender)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", u.Name);
                cmd.Parameters.AddWithValue("@Surname", u.Surname);
                cmd.Parameters.AddWithValue("@Age", u.Age);
                cmd.Parameters.AddWithValue("@Gender", u.Gender);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                    msg = "Kullanıcı başarıyla eklendi";
                else
                    msg = "Kullanıcı eklendi";
            }
            return msg;
        }

        [HttpPut]
        // PUT api/<controller>/5
        public string Put(int id, UserEntity u)
        {
            string msg = "";
            if (u != null)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Name = @Name, Surname = @Surname,Age =@Age,Gender = @Gender WHERE UserID = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Name", u.Name);
                cmd.Parameters.AddWithValue("@Surname", u.Surname);
                cmd.Parameters.AddWithValue("@Age", u.Age);
                cmd.Parameters.AddWithValue("@Gender", u.Gender);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                    msg = "Kullanıcı başarıyla güncellendi";
                else
                    msg = "Kullanıcı güncellenemedi";
            }
            return msg;
        }

        [HttpDelete]
        // DELETE api/<controller>/5
        public string Delete(int id)
        {
            string msg = "";
            SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID = @id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
                msg = "Kullanıcı başarıyla silindi";
            else
                msg = "Kullanıcı silinemedi";
            return msg;
        }
    }
}