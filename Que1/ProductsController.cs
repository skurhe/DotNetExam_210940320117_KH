using Que1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Que1.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> lst = new List<Product>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source= (localdb)\ProjectsV13; Initial Catalog = ExamDatabase; Integrated Security = True;";
            cn.Open();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Products;";

            try
            {
                SqlDataReader dr = cmdInsert.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(new Product { ProductId = (int)dr["ProductId"], ProductName = (string)dr["ProductName"], Rate = (decimal)dr["Rate"], Description = (string)dr["Description"], CategoryName = (string)dr["CategoryName"] });

                }
                dr.Close();
            }

            catch (Exception ex)
            {

            }

            finally {

                cn.Close();
            }

            return View(lst);

        }
                   

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            List<Product> pro = new List<Product>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source= (localdb)\ProjectsV13; Initial Catalog = ExamDatabase; Integrated Security = True;";
            cn.Open();
         
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Products where ProductId=@ProductId";
            cmdInsert.Parameters.AddWithValue("@ProductId", id);
            SqlDataReader dr = cmdInsert.ExecuteReader();
            Product obj = null;
            if (dr.Read())
            {
                obj = new Product { ProductId = id, ProductName = dr.GetString(1), Rate = dr.GetDecimal(2), Description = dr.GetString(3), CategoryName = dr.GetString(4) };
            }
            else {
                ViewBag.ErrorMessage = "Finish";
            }

            cn.Close();
            return View(obj);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product obj)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source= (localdb)\ProjectsV13; Initial Catalog = ExamDatabase; Integrated Security = True;";
            cn.Open();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "update Products set ProductName=@ProductName,Rate=@Rate, Description=@Description ,CategoryName=@CategoryName where ProductId=@ProductId";
            cmdInsert.Parameters.AddWithValue("@ProductId", id);
            cmdInsert.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmdInsert.Parameters.AddWithValue("@Rate", obj.Rate);
            cmdInsert.Parameters.AddWithValue("@Description", obj.Description);
            cmdInsert.Parameters.AddWithValue("@CategoryName", obj.CategoryName);

            try
            {
                // TODO: Add update logic here
                cmdInsert.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
            finally
            {
                cn.Close();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
