using Npgsql;
using OnlineCafe.Model;

namespace OnlineCafe.Controller
{
    public class RestaurantController
    {
        readonly ProductController productController = new();
        public int AddRestaurant(RestaurantI restaurant)
        {
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "INSERT INTO restaurant (restaurant_name, chef_name, servicecharge) VALUES (@name, @chef_name, @servicecharge) RETURNING restaurant_id";

            cmd.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Varchar, restaurant.Name!);
            cmd.Parameters.AddWithValue("chef_name", NpgsqlTypes.NpgsqlDbType.Varchar, restaurant.Chef!);
            cmd.Parameters.AddWithValue("servicecharge", NpgsqlTypes.NpgsqlDbType.Numeric, restaurant.Service!);

            int insertedId = (int)cmd.ExecuteScalar()!;

            return insertedId;
        }

        public void GetOne(int restaurantId)
        {
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM restaurant WHERE restaurant_id = @restaurantId", conn);
            cmd.Parameters.AddWithValue("restaurantId", restaurantId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                Console.WriteLine($" id: {reader["restaurant_id"]} Название: {reader["restaurant_name"]} / имя шефа: {reader["chef_name"]} /обслуживание: {reader["servicecharge"]}");
                
            }
        }
        public Boolean Getall()
        {

            Console.WriteLine("Все Рестораны");
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM restaurant", conn);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine($" id: {reader["restaurant_id"]}, Название: {reader["restaurant_name"]}, Шеф повар: {reader["chef_name"]}, Обслуживание: {reader["servicecharge"]}");

                while (reader.Read())
                {
                    Console.WriteLine($" id: {reader["restaurant_id"]}, Название: {reader["restaurant_name"]}, Шеф повар: {reader["chef_name"]}, Обслуживание: {reader["servicecharge"]}");
                }

                return true;
            }
            else
            {
                Console.WriteLine("Ресторанов нет");
                return false;
            }

        }


        public void DeleteRestaurant(RestaurantI restaurant)
        {
            Console.Clear();
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            // Удаление блюд по ID ресторана
            cmd.CommandText = "DELETE FROM dishes WHERE restaurant_id = @restaurantId";
            cmd.Parameters.AddWithValue("restaurantId", NpgsqlTypes.NpgsqlDbType.Integer, restaurant.id!);
            cmd.ExecuteNonQuery();

            // Удаление сотрудников по ID ресторана
            cmd.CommandText = "DELETE FROM employee WHERE restaurant_id = @restaurantId";
            cmd.ExecuteNonQuery();

            // Удаление самого ресторана
            cmd.CommandText = "DELETE FROM restaurant WHERE restaurant_id = @restaurantId";
            cmd.ExecuteNonQuery();
        }

        public void EditRestaurant(RestaurantI restaurant)
        {
            Console.Clear();
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE restaurant SET restaurant_name = @newName, service = @Newtype, chef_name = @newPrice  WHERE restaurant_id = @id";


            cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Integer, restaurant.id!);
            cmd.Parameters.AddWithValue("newName", NpgsqlTypes.NpgsqlDbType.Varchar, restaurant.Name!);
            cmd.Parameters.AddWithValue("newPrice", NpgsqlTypes.NpgsqlDbType.Varchar, restaurant.Service!);
            cmd.Parameters.AddWithValue("Newtype", NpgsqlTypes.NpgsqlDbType.Numeric, restaurant.Service!);
            cmd.ExecuteNonQuery();
        }


    }
    
}
