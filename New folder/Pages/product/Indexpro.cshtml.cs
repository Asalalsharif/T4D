using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.product
{
    public class IndexproModel : PageModel
    {
        public List<ProductInfo> listproduct = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS ;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM product";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.product_id = "" + reader.GetInt32(0);
                                productInfo.product_name = reader.GetString(1);
                                productInfo.location = reader.GetString(2);
                                productInfo.fees = reader.GetString(3);
                                productInfo.category_id = "" + reader.GetInt32(4);



                                listproduct.Add(productInfo);

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
    public class ProductInfo
    {
        public string product_id;
        public string product_name;
        public string location;
        public string fees;
        public string category_id;
        


    }

}



