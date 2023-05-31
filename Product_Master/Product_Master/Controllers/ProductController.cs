using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Product_Master.Models;


namespace Product_Master.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();

                    string dbConnectionStr = "";
                    if (!string.IsNullOrEmpty(dbconfig.ToString()))
                    {
                        dbConnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                        using (SqlConnection connection = new SqlConnection(dbConnectionStr))
                        {
                            string sql = "SP_Add_New_Product";
                            using (SqlCommand cmd = new SqlCommand(sql, connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                                cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                                cmd.Parameters.AddWithValue("@ProductCost", product.ProductCost);
                                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                                connection.Open();
                                cmd.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Product");
        }
    }
}