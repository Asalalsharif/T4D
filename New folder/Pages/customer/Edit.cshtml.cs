using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.customer
{
    public class EditModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id= Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customer WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.name = reader.GetString(1);
                                customerInfo.pass = reader.GetString(2);
                                customerInfo.email = reader.GetString(3);
                                customerInfo.phone = reader.GetString(4);
                                customerInfo.address = reader.GetString(5);
                                customerInfo.order_id = reader.GetString(6);


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
            customerInfo.id = Request.Form["id"];
            customerInfo.name = Request.Form["name"];
            customerInfo.pass = Request.Form["pass"];
            customerInfo.email = Request.Form["email"];
            customerInfo.phone = Request.Form["phone"];
            customerInfo.address = Request.Form["address"];
            customerInfo.order_id = Request.Form["order_id"];

            if (customerInfo.id.Length==0 || customerInfo.name.Length==0 || customerInfo.pass.Length == 0 || customerInfo.email.Length == 0 || customerInfo.phone.Length == 0 || customerInfo.address.Length == 0 || customerInfo.order_id.Length == 0)
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
                    String sql = "UPDATE customer SET name=@name, pass=@pass, email=@email, phone=@phone, address=@address, order_id=@order_id where id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue("@pass", customerInfo.pass);
                        command.Parameters.AddWithValue("@email", customerInfo.email);
                        command.Parameters.AddWithValue("@phone", customerInfo.phone);
                        command.Parameters.AddWithValue("@address", customerInfo.address);
                        command.Parameters.AddWithValue("@order_id", customerInfo.order_id);
                        command.Parameters.AddWithValue("@id", customerInfo.id);

                        command.ExecuteNonQuery();
                    }


                }

            }

            catch (Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/customer/Index");

        }

    }
}
