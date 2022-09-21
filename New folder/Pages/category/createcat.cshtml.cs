using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.category
{
    public class createcatcshtmlModel : PageModel
    {
        public CategoryInfo categoryInfo = new CategoryInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            categoryInfo.category_name = Request.Form["category_name"];
          
            


            if (categoryInfo.category_name.Length == 0)
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
                    string sql = "Insert into categories" +
                                  "(category_name) VALUES" +
                                  "(@category_name);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@category_name", categoryInfo.category_name);
                       
                        

                        command.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            categoryInfo.category_name = ""; 
            successMessage = "New Category had been added correctly";

            Response.Redirect("/category/Indexcat");



        }
    }
}
