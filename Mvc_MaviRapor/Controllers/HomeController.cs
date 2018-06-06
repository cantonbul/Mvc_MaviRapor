using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Mvc_MaviRapor.Controllers
{
    public class HomeController : Controller
    {
        DataSet ds = new DataSet();
        string constr = ConfigurationManager.ConnectionStrings["GumrukEntities"].ConnectionString;

        // GET: Home
        public ActionResult Index()
        {
            if (Session["user"]==null)
            {
                return RedirectToAction("Login", "Login");
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT " +
                " m.Ref,a.Ref,a.RefTarihi,a.TescilTarihi,a.Ithalatci,a.Ihracatci,a.YuklemeYer,a.MuayeneTuru,a.Kullanici, Count(a.Tescil_No) AS KalemSayisi " +
                " FROM [Gumruk].[dbo].BEYANNAME a " +
                " LEFT JOIN Gumruk.dbo.BEY_MalKalemleri m  ON a.Ref = m.Ref " +
                " where a.RefTarihi = '20180326' and a.Ref like 'ITH%' " +
                " GROUP BY m.Ref,a.Ref,a.RefTarihi,a.TescilTarihi,a.Ithalatci,a.Ihracatci,a.YuklemeYer,a.MuayeneTuru,a.Kullanici; ";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }

            return View(ds);
        }
        public ActionResult Anasayfa()
        {
            return View();
        }
        public ActionResult InsertTescil()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
             
                string query2 = "SELECT " +
                    " m.Ref,a.Ref,a.RefTarihi,a.TescilTarihi,a.Ithalatci,a.Ihracatci,a.YuklemeYer,a.MuayeneTuru,a.Kullanici, Count(a.Tescil_No) AS KalemSayisi " +
                    " FROM [Gumruk].[dbo].BEYANNAME a " +
                    " LEFT JOIN Gumruk.dbo.BEY_MalKalemleri m  ON a.Ref = m.Ref " +
                    " where CONVERT(datetime, a.TescilTarihi,105) between CONVERT(datetime,'" + Request.Form["tescilTrh1"] + "',105) and CONVERT(datetime,'" + Request.Form["tescilTrh2"] + "',105) and a.Ref like 'ANT%' and a.Kullanici = 'funda' " +
                    " GROUP BY m.Ref,a.Ref,a.RefTarihi,a.TescilTarihi,a.Ithalatci,a.Ihracatci,a.YuklemeYer,a.MuayeneTuru,a.Kullanici; ";
                using (SqlCommand cmd = new SqlCommand(query2))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            return View("Index", ds);
        }

       
    }
}