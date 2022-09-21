using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.category
{
    public class IndexModel : PageModel
    {
        public CategoryInfo categoryInfo = new CategoryInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String category_id = Request.Query["category_id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM categories WHERE category_id=@category_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@category_id", category_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                categoryInfo.category_id = "" + reader.GetInt32(0);
                                categoryInfo.category_name = reader.GetString(1);
                                


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
            categoryInfo.category_id = Request.Form["category_id"];
            categoryInfo.category_name = Request.Form["category_name"];


            if (categoryInfo.category_id.Length == 0 || categoryInfo.category_name.Length == 0)
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
                    String sql = "UPDATE categories SET category_name=@category_name where category_id=@category_id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@category_name", categoryInfo.category_name);
                        command.Parameters.AddWithValue("@category_id", categoryInfo.category_id);

                        command.ExecuteNonQuery();
                    }


                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/category/Indexcat");

        }

    }
}

