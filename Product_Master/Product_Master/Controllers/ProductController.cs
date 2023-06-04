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

        
     
        [HttpPost]
        public IActionResult ProductDelete(int id)
        {
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(dbconnectionStr))
            {
                connection.Open();

                string sql = "SP_Delete_Product_By_Id";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }

                connection.Close();
            }

            return RedirectToAction("Index");
        }
        private readonly string _dbconnectionStr;

        public ProductController(IConfiguration configuration)
        {
            _dbconnectionStr = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbconnectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Product_Master] WHERE Id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        con.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                ProductModel product = new ProductModel();
                                //product.Id = (int)dataTable.Rows[0]["Id"];
                                product.ProductName = (string)dataTable.Rows[0]["product_name"];
                                product.ProductDescription = (string)dataTable.Rows[0]["product_desc"];
                                product.ProductCost = (decimal)dataTable.Rows[0]["cost"];
                                product.Stock = (int)dataTable.Rows[0]["stock"];
                                return View(product);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductModel product)
        {
            using (SqlConnection con = new SqlConnection(_dbconnectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_Update_Product", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    cmd.Parameters.AddWithValue("@ProductCost", product.ProductCost);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return View(product);
        }
    }
}
 
