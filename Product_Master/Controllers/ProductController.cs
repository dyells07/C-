using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
//using utilities;
using System.IO;
using Product_Master.Models;
using System.Data.Common;

namespace Product_Master.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<ProductModel> productList = new List<ProductModel>();
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbConnectionStr = "";
            try
            {
                string sql = "[dbo].[SP_Get_ProductList]";
                dbConnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(dbConnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ProductModel product = new ProductModel();
                            product.Id = Convert.ToInt32(dataReader["Id"]);
                            product.ProductName = Convert.ToString(dataReader["ProductName"]);
                            product.ProductDescription = Convert.ToString(dataReader["ProductDescription"]);
                            product.ProductCost = Convert.ToDecimal(dataReader["ProductCost"]);
                            product.Stock = Convert.ToInt32(dataReader["Stock"]);
                            productList.Add(product);



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(productList);

        }
        //[dbo].[SP_Get_Product_By_Id]
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
            ProductModel product= new ProductModel();
            using(SqlConnection connection = new SqlConnection(dbconnectionStr))
            {
                string sql = "USP_Update_Product";
                using (SqlCommand cmd = new SqlCommand(sql,connection)) 
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader datareader = cmd.ExecuteReader()) 
                    {
                        while (datareader.Read()) 
                        {
                            product.Id = Convert.ToInt32(datareader["id"]);
                            product.ProductName = Convert.ToString(datareader["ProductName"]);
                            product.ProductDescription=Convert.ToString(datareader.GetString("ProductDescription"));
                            product.ProductCost = Convert.ToDecimal(datareader["ProductCost"]);
                            product.Stock = Convert.ToInt32(datareader["Stock"]);
                        
                        }
                    
                    }
                
                }
                connection.Close();

            }
            return View(product);
            
        }
        [HttpPost]
        public IActionResult Edit(ProductModel product) 
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json").Build();
                    if(!string.IsNullOrEmpty(dbconfig.ToString()))
                    {
                        var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                        using(SqlConnection connection=new SqlConnection(dbconnectionStr))
                        {
                            string sql = "SP_Update_Product";
                            using (SqlCommand cmd=new SqlCommand(sql,connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@id", product.Id);
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
            catch
            {
                throw;
            }
            return RedirectToAction("Index");
        
        }
    }
}