using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginPageDemo1.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace LoginPageDemo1.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginClass lc) {
            string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainConn);
            string sqlquery = "select username,[password] from [User] where username = @username and password = @password";
            sqlconn.Open();
            SqlCommand sqlComm = new SqlCommand(sqlquery,sqlconn);
            sqlComm.Parameters.AddWithValue("@username", lc.UserName);
            sqlComm.Parameters.AddWithValue("@password", lc.Password);
            SqlDataReader sdr = sqlComm.ExecuteReader();
            if (sdr.Read()) {
                Session["Username"] = lc.UserName.ToString();
                return RedirectToAction("welcome");
            }
            else {
                ViewData["Message"] = "User Login Detail Fail!";
            }
            sqlconn.Close();
            return View();
        }
        public ActionResult welcome() {
            return View();
        }
    }
}