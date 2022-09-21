using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace TD4.Pages.product
{
    public class EditproModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String product_id = Request.Query["product_id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM product WHERE product_id=@product_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@product_id", product_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                productInfo.product_id = "" + reader.GetInt32(0);
                                productInfo.product_name = reader.GetString(1);
                                productInfo.location = reader.GetString(2);
                                productInfo.fees = reader.GetString(3);
                                productInfo.category_id = "" + reader.GetInt32(4);



                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }


        }
        public void OnPost()
        {
            productInfo.product_id = Request.Form["product_id"];
            productInfo.product_name = Request.Form["product_name"];
            productInfo.location = Request.Form["location"];
            productInfo.fees = Request.Form["fees"];
            productInfo.category_id = Request.Form["category_id"];


            if (productInfo.product_id.Length == 0 || productInfo.product_name.Length == 0 || productInfo.location.Length == 0 || productInfo.fees.Length == 0 || productInfo.category_id.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE product SET product_name=@product_name , location=@location , fees=@fees, category_id=@category_id where product_id=@product_id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@product_name", productInfo.product_name);
                        command.Parameters.AddWithValue("@location", productInfo.location);
                        command.Parameters.AddWithValue("@fees", productInfo.fees);           
                        command.Parameters.AddWithValue("@category_id", productInfo.category_id);
                        command.Parameters.AddWithValue("@product_id", productInfo.product_id);

                        command.ExecuteNonQuery();
                    }


                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/product/Indexpro");

        }

    }
}
