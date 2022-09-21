using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.customer
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> listCustomer = new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM customer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.name = reader.GetString(1);
                                customerInfo.pass = reader.GetString(2);
                                customerInfo.email = reader.GetString(3);
                                customerInfo.phone = reader.GetString(4);
                                customerInfo.address = reader.GetString(5);
                                customerInfo.order_id = reader.GetString(6);

                                listCustomer.Add(customerInfo);

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
    public class CustomerInfo
    {
        public string id;
        public string name;
        public string pass;
        public string email;
        public string phone;
        public string address;
        public string order_id;
        
    }

}

