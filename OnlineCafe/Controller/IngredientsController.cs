
using Newtonsoft.Json;
using Npgsql;
using OnlineCafe.Model;
using System.Diagnostics;
using System.Text;

namespace OnlineCafe.Controller
{
    public class IngredientsController : Product
    {
        public List<Ingredients> productData = [];
        public List<decimal> sumprice = [], sumweight = [];
        readonly ProductController Controller = new ProductController();

        public List<Ingredients> ProductSelection(int productID, decimal weight)
        {
            using var conn = new NpgsqlConnection(Controller.connString);
            conn.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT name, price,product_type FROM product WHERE id = @productId";

            cmd.Parameters.AddWithValue("productId", NpgsqlTypes.NpgsqlDbType.Integer, productID);

            using var reader = cmd.ExecuteReader();
           
            if (reader.Read())
            {
                string productName = reader.GetString(0);
                decimal productPrice = reader.GetDecimal(1);
                string product_type = reader.GetString(2);

                sumprice.Add(productPrice * weight);
                sumweight.Add(weight);
                Ingredients ingredients = new Ingredients(productName,weight,product_type);
                productData.Add(ingredients);

            }
            else
            {
                Console.WriteLine("Продукт с указанным ID не найден.");
            }
            return productData;
            
        }

       
    }
}



