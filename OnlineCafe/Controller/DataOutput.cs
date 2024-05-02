using Npgsql;
using OnlineCafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCafe.Controller
{
    
    public class DataOutput
    {
        ProductController productController = new();
        string filePath = @"dataoutput.txt";
        public void WriteAllDishesToFile()
        {
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT d.name, d.price, r.restaurant_name AS restaurant_name, r.chef_name AS chef_name FROM dishes d JOIN restaurant r ON d.restaurant_id = r.restaurant_id ", conn);

            using var reader = cmd.ExecuteReader();

            using StreamWriter writer = new StreamWriter(filePath);

            while (reader.Read())
            {
                string dishName = reader["name"].ToString()!;
                decimal dishPrice = (decimal)reader["price"];
                string restaurantName = reader["restaurant_name"].ToString()!;
                string chefName = reader["chef_name"].ToString()!;
                writer.WriteLine($"Блюдо: {dishName}, Стоимость: {dishPrice}, Ресторан: {restaurantName}, Шеф-повар: {chefName}");
            }
        }

    }
}
