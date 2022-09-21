using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TD4.Pages.customer
{
    public class createModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            customerInfo.name = Request.Form["name"];
            customerInfo.pass = Request.Form["pass"];
            customerInfo.email = Request.Form["email"];
            customerInfo.phone= Request.Form["phone"];
            customerInfo.address = Request.Form["address"];
            customerInfo.order_id = Request.Form["order_id"];



            if (customerInfo.name.Length == 0 || customerInfo.pass.Length == 0 || customerInfo.email.Length == 0 || customerInfo.phone.Length == 0 || customerInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TDS;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Insert into customer" +
                                  "(name, pass, email, phone, address, order_id) VALUES" +
                                  "(@name, @pass, @email, @phone, @address, @order_id);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue("@pass", customerInfo.pass);
                        command.Parameters.AddWithValue("@email", customerInfo.email);
                        command.Parameters.AddWithValue("@phone", customerInfo.phone);
                        command.Parameters.AddWithValue("@address", customerInfo.address);
                        command.Parameters.AddWithValue("@order_id", customerInfo.order_id);

                        command.ExecuteNonQuery();
                    }


                }
            
            }
            catch (Exception ex)
            {
            errorMessage=ex.Message;
                return;
            }


            customerInfo.name = ""; customerInfo.pass = ""; customerInfo.email = ""; customerInfo.phone = ""; customerInfo.address = "";
            successMessage = "New Customer had been added correctly";

            Response.Redirect("/customer/Index");



        }
    }
}
