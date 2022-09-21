using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace TD4.Pages.order
{
    public class createordModel : PageModel
    {
        public OrderInfo orderInfo = new OrderInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            orderInfo.product_id = Request.Form["product_id"];
            orderInfo.customer_id = Request.Form["customer_id"];
            orderInfo.fees = Request.Form["fees"];
         

            if (orderInfo.product_id.Length == 0 || orderInfo.customer_id.Length == 0 || orderInfo.fees.Length == 0)
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
                    string sql = "Insert into orders" +
                                  "(product_id, customer_id, fees) VALUES" +
                                  "(@product_id , @customer_id, @fees);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@product_id", orderInfo.product_id);
                        command.Parameters.AddWithValue("@customer_id", orderInfo.customer_id);
                        command.Parameters.AddWithValue("@fees", orderInfo.fees);
                        
                        command.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            orderInfo.product_id = "";
            orderInfo.customer_id = "";
            orderInfo.fees = "";
            
            successMessage = "New order had been added correctly";

            Response.Redirect("/order/Indexord");



        }
    }
}