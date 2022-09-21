using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.order
{
    public class IndexordModel : PageModel
    {
        public List<OrderInfo> listorder = new List<OrderInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS ;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM orders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderInfo orderInfo = new OrderInfo();
                                orderInfo.order_id = "" + reader.GetInt32(0);
                                orderInfo.product_id = "" + reader.GetInt32(1);
                                orderInfo.customer_id = "" + reader.GetInt32(2);
                                orderInfo.fees = reader.GetString(3);
                                



                                listorder.Add(orderInfo);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }

    }
    public class OrderInfo
    {
        public string order_id;
        public string product_id;
        public string customer_id;
        public string fees;
        



    }

}


