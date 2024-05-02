using Newtonsoft.Json;
using OnlineCafe.Controller;
using OnlineCafe.Model;
using System.Net.Http.Headers;


namespace OnlineCafe.View
{
    static public class Program
    {
        public static void Main(string[] args)
        {
            Product product = new();
            ProductController controller = new();
            DishesController dishesController = new();
            IngredientsController ingredients = new();
            RestaurantI restaurant = new();
            EmployeeController employeeController = new();
            Employees employees = new();
            RestaurantController restaurantController = new();
            Dishes dishes = new();
            DataOutput dataOutput = new();


        Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Добро пожаловать господин");
            Console.ResetColor();
        makers_mainMenu:
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("ЧТо желаете :)");
            string input = InputHelper.GetValueFromConsole("1.Посмотреть рестораны\n2.Посмотреть все блюда из всех рестаранов\n3.Посмотреть часто используемый тип продукта\n4.Отчет txt", "1", "2", "3", "4");
            switch (input)
            {
                case "1":
                makers_main2Menu:
                    Console.Clear();
                    string input2 = restaurantController.Getall() ? InputHelper.GetValueFromConsole("1.Добавить ресторан\n2.Удалить ресторан\n3.Изменить ресторан\n4.Зайти в Ресторан\n0.назад", "1", "2", "3", "4", "0") : InputHelper.GetValueFromConsole("1.Добавьте ресторан\n0.назад", "1", "0");

                    switch (input2)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Название ресторана");
                            restaurant.Name = Console.ReadLine();
                            Console.WriteLine("Имя Шеф повара");
                            employees.Name = Console.ReadLine();
                            employees.position = "Шеф повар";
                            restaurant.Chef = employees.Name;
                            Console.WriteLine("обслуживание");
                            decimal service = Convert.ToDecimal(Console.ReadLine());
                            restaurant.Service = service / 100;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Clear();
                            Console.WriteLine("Обработка данных");
                            Console.WriteLine("пожалуста подожди");
                            Console.ResetColor();
                            employees.restaurant_id = restaurantController.AddRestaurant(restaurant);
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine($"зарплата вашего шефа {restaurant.Chef}");
                            employees.salary = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Начало его смены");
                            employees.start_schedule = Console.ReadLine();
                            Console.WriteLine("Конец его смены");
                            employees.end_schedule = Console.ReadLine();
                            employeeController.AddEmployees( employees );
                            restaurantController.GetOne((int)employees.restaurant_id);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Ваш новый ресторан поздравляю");
                            Console.ResetColor();
                            string input3 = InputHelper.GetValueFromConsole("1.Добавить блюда в ваш ресторан\n2.Добавить сотрудников\n0.назад", "1", "2", "0");
                            switch (input3)
                            {
                                case "1":

                                makers_mainMenu12:
                                    Console.Clear();
                                    Console.WriteLine("Добавление блюда");
                                    Console.WriteLine("Укажите название");
                                    dishes.Name = Console.ReadLine();

                                    int productid = -1;

                                akers_mainMenu1:
                                    Console.Clear();
                                    controller.GetAll();

                                    Console.WriteLine("Выбери продукт для ингредиента");
                                    Console.WriteLine("Чтобы выйти нажмите 0");
                                    productid = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Сколько кг возмешь");
                                    decimal Gram2 = Convert.ToDecimal(Console.ReadLine());
                                    ingredients.productData = ingredients.ProductSelection(productid, Gram2);
                                    string json = JsonConvert.SerializeObject(ingredients.productData);
                                    dishes.Ingredients = json;
                                    if (productid != 0) goto akers_mainMenu1;

                                    Console.WriteLine("Укажите цену");

                                    while (true)
                                    {
                                        dishes.Price = Convert.ToDecimal(Console.ReadLine());
                                        if (dishes.Price > ingredients.sumprice.Sum() * 2)
                                        {
                                            Console.WriteLine($"Сумма не должна вдое привешать себестоимость: {ingredients.sumprice.Sum()}");

                                        }
                                        if (dishes.Price < ingredients.sumprice.Sum())
                                        {
                                            Console.WriteLine($"Цена не должна быть меньше себестоимости: {ingredients.sumprice.Sum()}");
                                        }
                                        if (dishes.Price < ingredients.sumprice.Sum() * 2 && dishes.Price > ingredients.sumprice.Sum())
                                        {
                                            break;
                                        }
                                    }
                                    dishes.weight = ingredients.sumweight!.Sum();
                                    dishes.restaurant_id = employees.restaurant_id;
                                    dishesController.AddDishes(dishes);

                                    dishesController.GetallFromres(dishes);
                                    switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                                    {
                                        case "1":
                                            goto makers_mainMenu12;
                                        case "2":
                                            goto makers_main2Menu;
                                    }

                                    break;
                                case "2":
                                    makers_mainMenu13:
                                    Console.Clear();
                                    Console.WriteLine("Добавьте сотрудника");

                                    Console.WriteLine("Имя сотрудника");
                                    employees.Name = Console.ReadLine();
                                    Console.WriteLine("должность\n1.повар\n2.офик\n3.менеджер");
                                    employees.position = Console.ReadLine();
                                    switch (employees.position)
                                    {
                                        case "1":
                                            employees.position = "Шеф повар";
                                            break;
                                        case "2":
                                            employees.position = "Официант";
                                            break;
                                        case "3":
                                            employees.position = "Менеджер";
                                            break;
                                    }

                                    Console.WriteLine("зарплата");
                                    employees.salary = Convert.ToDecimal(Console.ReadLine());
                                    Console.WriteLine("Начало его смены");
                                    employees.start_schedule = Console.ReadLine();
                                    Console.WriteLine("Конец его смены");
                                    employees.end_schedule = Console.ReadLine();
                                    int a = (int)employees.restaurant_id;
                                    employees.restaurant_id = a;
                                     
                                    employeeController.AddEmployees(employees);
                                    Console.Clear() ;
                                    employeeController.GetAllFromRestaurant(a);
                                    switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                                    {
                                        case "1":
                                            goto makers_mainMenu13;
                                        case "2":
                                            goto makers_main2Menu;
                                    }
                                    break;
                                case "0":
                                    Console.Clear();
                                    goto makers_main2Menu;
                            }

                            break;

                        case "2":
                            makers_mainMenu10:
                            Console.Clear();
                            restaurantController.Getall();
                            Console.WriteLine("Выберите id ресторана что бы удалить");
                            restaurant.id = Convert.ToInt32(Console.ReadLine());
                            restaurantController.DeleteRestaurant(restaurant);
                            switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                            {
                                case "1":
                                    goto makers_mainMenu10;
                                case "2":
                                    goto makers_main2Menu;
                            }
                            break;
                        case "3":
                        makers_mainMenu11:
                            Console.Clear();
                            restaurantController.Getall();
                            Console.WriteLine("Изменение ресторана");
                            Console.WriteLine("ведите id Ресторана");
                            restaurant.id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Название ресторана");
                            restaurant.Name = Console.ReadLine();
                            Console.WriteLine("Изменените Имя Шеф повара");
                            employees.Name = Console.ReadLine();
                            employees.position = "Шеф повар";
                            restaurant.Chef = employees.Name;
                            Console.WriteLine("обслуживание");
                            service = Convert.ToDecimal(Console.ReadLine());
                            restaurant.Service = service / 100;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Clear();
                            Console.WriteLine("Обработка данных");
                            Console.WriteLine("пожалуста подожди");
                            Console.ResetColor();
                            restaurantController.EditRestaurant(restaurant);
                            employees.restaurant_id = restaurant.id;
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine($"зарплата вашего шефа {restaurant.Chef}");
                            employees.salary = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Начало его смены");
                            employees.start_schedule = Console.ReadLine();
                            Console.WriteLine("Конец его смены");
                            employees.end_schedule = Console.ReadLine();
                            employeeController.AddEmployees(employees);
                            restaurantController.GetOne((int)employees.restaurant_id);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Ваш  ресторан изменен поздравляю");
                            Console.ResetColor();
                            switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                            {
                                case "1":
                                    goto makers_mainMenu11;
                                case "2":
                                    goto makers_main2Menu;
                            }
                            break;
                        case "4":

                            Console.Clear();
                            restaurantController.Getall();
                            Console.WriteLine("Ведите id Ресторана");
                            int res_id = Convert.ToInt32(Console.ReadLine());
                            restaurantController.GetOne(res_id);    
                            makers_mainMenu3:
                            string input6 = InputHelper.GetValueFromConsole("1.посмотреть меню\n2.посмотреть сотрудников\n0.назад", "1", "2", "0");
                            switch (input6)
                            {
                                case "1":
                                    dishes.restaurant_id = res_id;
                                    makers_mainMenu8:
                                    Console.Clear();
                                    string input7 = dishesController.GetallFromres(dishes)? InputHelper.GetValueFromConsole("1.Добавить блюда\n2.Измените блюда\n3.Удалите блюда\n0.Назад", "1", "2", "3", "0"): InputHelper.GetValueFromConsole("1.Добавьте блюда\n0.Назад", "1", "0");
                                    switch (input7)
                                    {
                                        case "1":
                                            makers_mainMenu9:
                                            Console.Clear();
                                            Console.WriteLine("Добавление блюда");
                                            Console.WriteLine("Укажите название");
                                            dishes.Name = Console.ReadLine();

                                            int productid = -1;

                                            akers_mainMenu9:
                                                Console.Clear();
                                                controller.GetAll();

                                                Console.WriteLine("Выбери продукт для ингредиента");
                                                Console.WriteLine("Чтобы выйти нажмите 0");
                                                productid = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Сколько кг возмешь");
                                                decimal Gram2 = Convert.ToDecimal(Console.ReadLine());
                                                ingredients.productData = ingredients.ProductSelection(productid, Gram2);
                                                string json = JsonConvert.SerializeObject(ingredients.productData);
                                                dishes.Ingredients = json;
                                             if (productid != 0) goto akers_mainMenu9;

                                            Console.WriteLine("Укажите цену");

                                            while (true)
                                            {
                                                dishes.Price = Convert.ToDecimal(Console.ReadLine());
                                                if (dishes.Price > ingredients.sumprice.Sum() * 2)
                                                {
                                                    Console.WriteLine($"Сумма не должна вдое привешать себестоимость: {ingredients.sumprice.Sum()}");

                                                }
                                                if (dishes.Price < ingredients.sumprice.Sum())
                                                {
                                                    Console.WriteLine($"Цена не должна быть меньше себестоимости: {ingredients.sumprice.Sum()}");
                                                }
                                                if(dishes.Price < ingredients.sumprice.Sum() * 2 && dishes.Price > ingredients.sumprice.Sum())
                                                {
                                                    break;
                                                }
                                            }
                                            dishes.weight = ingredients.sumweight!.Sum();
                                            dishes.restaurant_id = res_id;
                                            dishesController.AddDishes(dishes);
                                            Console.Clear();
                                            dishesController.GetallFromres(dishes);
                                            switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                                            {
                                                case "1":
                                                    goto makers_mainMenu9;
                                                case "2":
                                                    goto makers_mainMenu8;
                                            }
                                            break;
                                        case "2":
                                            makers_mainMenu7:
                                            Console.Clear();
                                            dishesController.Getall();
                                                Console.WriteLine("Изменить блюда");
                                                Console.WriteLine("выберите блюда по id");
                                                dishes.id = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Измените название");
                                                dishes.Name = Console.ReadLine();

                                        akers_mainMenu8:
                                            Console.Clear();
                                            controller.GetAll();

                                            Console.WriteLine("Выбери продукт для ингредиента");
                                            Console.WriteLine("Чтобы выйти нажмите 0");
                                            productid = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Сколько кг возмешь");
                                            decimal Gram3 = Convert.ToDecimal(Console.ReadLine());
                                            ingredients.productData = ingredients.ProductSelection(productid, Gram3);
                                            json = JsonConvert.SerializeObject(ingredients.productData);
                                            dishes.Ingredients = json;
                                            if (productid != 0) goto akers_mainMenu8;


                                            while (true)
                                            {
                                                dishes.Price = Convert.ToDecimal(Console.ReadLine());
                                                if (dishes.Price > ingredients.sumprice.Sum() * 2)
                                                {
                                                    Console.WriteLine($"Сумма не должна вдое привешать себестоимость: {ingredients.sumprice.Sum()}");

                                                }
                                                if (dishes.Price < ingredients.sumprice.Sum())
                                                {
                                                    Console.WriteLine($"Цена не должна быть меньше себестоимости: {ingredients.sumprice.Sum()}");
                                                }
                                                if (dishes.Price < ingredients.sumprice.Sum() * 2 && dishes.Price > ingredients.sumprice.Sum())
                                                {
                                                    break;
                                                }
                                            }
                                            dishes.weight = ingredients.sumweight!.Sum();
                                            dishes.restaurant_id = res_id;
                                            dishesController.EditDishes(dishes);
                                            Console.Clear();
                                            dishesController.GetallFromres(dishes);
                                            switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                                            {
                                                case "1":
                                                    goto makers_mainMenu7;
                                                case "2":
                                                    goto makers_mainMenu8;
                                            }
                                            break;
                                          case "3":
                                            Console.Clear();
                                            dishesController.GetallFromres(dishes);
                                            dishes.id = Convert.ToInt32(Console.ReadLine());
                                            dishesController.DeleteDishes(dishes);
                                              break;
                                          case "0":
                                            goto makers_mainMenu3;
                                      }
                                      break;
                                  case "2":
                                makers_mainMenu4:
                                    Console.Clear();
                                    string input8 = employeeController.GetAllFromRestaurant(res_id) ? InputHelper.GetValueFromConsole("1.Добавить сотрудника\n2.Удалите сотрудника\n0.Назад", "1", "2", "0") : InputHelper.GetValueFromConsole("1.Добавьте сотрудника\n0.Назад", "1", "0");
                                    switch  (input8)
                                      {
                                        
                                          case "1":
                                        makers_mainMenu5:
                                            Console.Clear();
                                            Console.WriteLine("Добавьте сотрудника");

                                              Console.WriteLine("Имя сотрудника");
                                              employees.Name = Console.ReadLine();
                                              Console.WriteLine("должность\n1.повар\n2.офик\n3.менеджер");
                                              employees.position = Console.ReadLine();
                                              switch (employees.position)
                                              {
                                                  case "1":
                                                      employees.position = "повар";
                                                      break;
                                                  case "2":
                                                      employees.position = "Официант";
                                                      break;
                                                  case "3":
                                                      employees.position = "Менеджер";
                                                      break;
                                              }

                                              Console.WriteLine("зарплата");
                                              employees.salary = Convert.ToDecimal(Console.ReadLine());
                                              Console.WriteLine("Начало его смены");
                                              employees.start_schedule = Console.ReadLine();
                                              Console.WriteLine("Конец его смены");
                                              employees.end_schedule = Console.ReadLine();
                                              employees.restaurant_id = res_id;
                                              employeeController.AddEmployees(employees);
                                            Console.Clear();
                                            employeeController.GetAllFromRestaurant(res_id);
                                            
                                            switch(InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                                            {
                                                case "1":
                                                    goto makers_mainMenu5;
                                                    case "2":
                                                    goto makers_mainMenu4;
                                            }
                                              break;
                                          case "2":
                                            makers_mainMenu6:
                                            Console.Clear();
                                            employeeController.GetAllFromRestaurant(res_id);
                                            Console.WriteLine("Удаление сотрудника");
                                            Console.WriteLine("Ведите id сотрудника");
                                            employees.id = Convert.ToInt32(Console.ReadLine());
                                            int a1 = (int)employees.restaurant_id!;

                                            employeeController.DeleteDishes(employees);
                                            Console.Clear();
                                            employeeController.GetAllFromRestaurant(a1);

                                            switch (InputHelper.GetValueFromConsole("Хотите продолжить\n1.ДА\n2.нет", "1", "2"))
                                            {
                                                case "1":
                                                    goto makers_mainMenu6;
                                                case "2":
                                                    goto makers_mainMenu4;
                                            }
                                            break;
                                          case "0":
                                              goto makers_mainMenu3;

                                      }
                                      break;
                                  case "0":
                                      goto makers_main2Menu;

                              }
                              break;

                        case "0":
                         goto makers_mainMenu;
                      }

                      break;
                  case "2":
                      dishesController.Getall();
                      string input5 = InputHelper.GetValueFromConsole("0.Назад", "0");
                      switch (input5)
                      {
                          case "0":
                              goto makers_mainMenu;
                      }
                      break;
                  case "3":
                 

                    try
                    {
                        var mostFrequentIngredient = controller.FrequentlyUsedProductFromRestaurant().GroupBy(x => x)
                                                      .OrderByDescending(g => g.Count())
                                                      .FirstOrDefault();
                        string mostFrequentIngredientValue = mostFrequentIngredient!.Key;
                        Console.WriteLine(mostFrequentIngredientValue);
                    }
                    catch(NullReferenceException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка\nВероятно нет  блюд ");
                        Console.ResetColor();
                    }
                      
                    switch (InputHelper.GetValueFromConsole("0.Назад", "0"))
                    {
                        case "0":
                            goto makers_mainMenu;
                    }
                    break;
                  case "4":
                      Console.WriteLine("Посмотрите в файле dataoutput.txt по пути C:\\Users\\user\\OneDrive\\Рабочий стол\\репозиторки\\OnlineCafe\\bin\\Debug\\net8.0");
                      dataOutput.WriteAllDishesToFile();
                      break;
              }


         }
    }
}