using Newtonsoft.Json;
using Npgsql;
using OnlineCafe.Model;

namespace OnlineCafe.Controller
{
    public class DishesController
    {
        ProductController productController = new();
        

      

        public void AddDishes(Dishes dishes)
        {

            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

           
            cmd.CommandText = "INSERT INTO dishes (name,price,ingredients,weight,restaurant_id) VALUES (@name,@price,@ingredients,@weight,@restaurant_id)";

            cmd.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Varchar, dishes.Name!);
            cmd.Parameters.AddWithValue("price", NpgsqlTypes.NpgsqlDbType.Numeric, dishes.Price!);
            cmd.Parameters.AddWithValue("ingredients", NpgsqlTypes.NpgsqlDbType.Json, dishes.Ingredients!);
            cmd.Parameters.AddWithValue("weight", NpgsqlTypes.NpgsqlDbType.Numeric, dishes.weight!);
            cmd.Parameters.AddWithValue("restaurant_id", NpgsqlTypes.NpgsqlDbType.Integer, dishes.restaurant_id!);

            cmd.ExecuteNonQuery();
        }
        public Boolean GetallFromres(Dishes dishes)
        {
            Console.WriteLine($"Все блюда из ресторана с id {dishes.restaurant_id}");
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT d.id, d.name, d.price, d.ingredients, d.weight FROM dishes d WHERE d.restaurant_id = @restaurantId", conn);
            cmd.Parameters.AddWithValue("restaurantId", dishes.restaurant_id!);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                do
                {
                    Console.WriteLine($" id: {reader["id"]}, Название: {reader["name"]}, Цена: {reader["price"]},\n состав: {reader["ingredients"]},\n Грамовка: {reader["weight"]} ");
                } while (reader.Read());
                return true;
            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("В выбранном ресторане нет блюд");
                Console.ResetColor();
                return false;

            }
        }
        public void Getall()
        {
            Console.WriteLine("Все Блюда");
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT d.id, d.name, d.price, d.ingredients, d.weight, r.restaurant_name AS restaurant_name FROM dishes d LEFT JOIN restaurant r ON d.restaurant_id = r.restaurant_id", conn);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                do
                {
                    Console.WriteLine($" id: {reader["id"]}, Название: {reader["name"]}, Цена: {reader["price"]},\n состав: {reader["ingredients"]},\n Грамовка: {reader["weight"]},\n из Ресторана: {reader["restaurant_name"]}");
                } while (reader.Read());
            }
            else
            {
                Console.WriteLine("В этом ресторане нет блюд");
            }
        }

        public void DeleteDishes(Dishes dishes)
        {
            Console.Clear();
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM dishes WHERE id = @value";


            cmd.Parameters.AddWithValue("value", NpgsqlTypes.NpgsqlDbType.Integer, dishes.id!);

            int rowsAffected = cmd.ExecuteNonQuery();

        }
        public void EditDishes(Dishes dishes)
        {
            Console.Clear();
            using var conn = new NpgsqlConnection(productController.connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE dishes SET name = @newName, ingredients = @ingredients, price = @price, weight = @weight  WHERE id = @id";
            string jsonString = JsonConvert.SerializeObject(dishes.Ingredients);
            cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Integer, dishes.id!);
            cmd.Parameters.AddWithValue("newName", NpgsqlTypes.NpgsqlDbType.Varchar, dishes.Name!);
            cmd.Parameters.AddWithValue("price", NpgsqlTypes.NpgsqlDbType.Numeric, dishes.Price!);
            cmd.Parameters.AddWithValue("ingredients", NpgsqlTypes.NpgsqlDbType.Json, jsonString);
            cmd.Parameters.AddWithValue("weight", NpgsqlTypes.NpgsqlDbType.Numeric, dishes.weight!);

            cmd.ExecuteNonQuery();
        }

    }
}
