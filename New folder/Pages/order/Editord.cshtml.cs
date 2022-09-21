using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using TD4.Pages.product;

namespace TD4.Pages.order
{
    public class EditordModel : PageModel
    {
        public OrderInfo orderInfo = new OrderInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String order_id = Request.Query["order_id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM orders WHERE order_id=@order_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@order_id", order_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                orderInfo.order_id = "" + reader.GetInt32(0);
                                orderInfo.product_id = "" + reader.GetInt32(1);
                                orderInfo.customer_id = "" + reader.GetInt32(2);
                                orderInfo.fees = reader.GetString(3);
                                



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
            orderInfo.order_id = Request.Form["order_id"];
            orderInfo.product_id = Request.Form["product_id"];
            orderInfo.customer_id = Request.Form["customer_id"];
            orderInfo.fees = Request.Form["fees"];
           
            if (orderInfo.order_id.Length == 0 || orderInfo.product_id.Length == 0 || orderInfo.customer_id.Length == 0 || orderInfo.fees.Length == 0)
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
                    String sql = "UPDATE orders SET product_id=@product_id , customer_id=@customer_id , fees=@fees where order_id=@order_id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@product_id", orderInfo.product_id);
                        command.Parameters.AddWithValue("@customer_id", orderInfo.customer_id);
                        command.Parameters.AddWithValue("@fees", orderInfo.fees);                       
                        command.Parameters.AddWithValue("@order_id", orderInfo.order_id);

                        command.ExecuteNonQuery();
                    }


                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/order/Indexord");

        }

    }
}
