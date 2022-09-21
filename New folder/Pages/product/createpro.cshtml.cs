using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TD4.Pages.category;

namespace TD4.Pages.product
{
    public class createproModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            productInfo.product_name = Request.Form["product_name"];
            productInfo.location = Request.Form["location"];
            productInfo.fees = Request.Form["fees"];
            productInfo.category_id = Request.Form["category_id"];




            if (productInfo.product_name.Length == 0 || productInfo.location.Length == 0 || productInfo.fees.Length == 0|| productInfo.category_id.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS ;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Insert into product" +
                                  "(product_name, location, fees, category_id) VALUES" +
                                  "(@product_name , @location, @fees, @category_id);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@product_name", productInfo.product_name);
                        command.Parameters.AddWithValue("@location", productInfo.location);
                        command.Parameters.AddWithValue("@fees", productInfo.fees);
                        command.Parameters.AddWithValue("@category_id", productInfo.category_id);



                        command.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            productInfo.product_name = "";
            productInfo.location = "";
            productInfo.fees = "";
            productInfo.category_id = "";
            successMessage = "New Product had been added correctly";

            Response.Redirect("/product/Indexpro");



        }
    }
}