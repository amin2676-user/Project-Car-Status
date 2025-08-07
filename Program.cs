using System;
using async.Vechicle.Details;
using async.Vechicle.Types;
using async.Vechicle;
using System.IO;
using System.Text.Json;

namespace mainNameSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clear the status.json file on Desktop ....
            string statusFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "status.json"
            );
            File.WriteAllText(statusFilePath, JsonSerializer.Serialize(new List<object>(), new JsonSerializerOptions { WriteIndented = true }));

            int model = AppReadLineInt("Please enter a number to choose your car (BENZ ==> 0 OR BMW ==> 1): ", 0, 1);
            CAR car = GenerateNewCarFromInt(model);

            if (car is IFeaturesOfTheCar featuresCar)
            {
                while (true)
                {
                    Console.WriteLine("\nAvailable features:");
                    //Console.WriteLine("1. Check Fuel");
                    //Console.WriteLine("2. Check Current Speed");
                    Console.WriteLine("3. Gas (Accelerate)");     //به این نیازی نیست هنوز خوب طراحی نشده 
                    Console.WriteLine("4. Brake");
                    Console.WriteLine("5. Open Fuel Cap");
                    Console.WriteLine("6. Close Fuel Cap");
                    Console.WriteLine("7. Refuel");
                    Console.WriteLine("8. Toggle Power Switch");
                    Console.WriteLine("9. Start Driving");
                    Console.WriteLine("0. Exit");

                    int result = AppReadLineInt("Please enter a command: ", 0, 10);
                    switch (result)
                    {
                        
                            
                        case 1:
                            featuresCar.LugicCheckCurrentSpeed().Wait();
                            break;
                        case 2:
                            featuresCar.LugicCheckCurrentSpeed().Wait();
                            break;
                        case 3:
                            featuresCar.IncreaseSpeed().Wait(); 
                            break;
                        case 4:
                            featuresCar.ReduceSpeed().Wait();   
                            break;
                        case 5:
                            featuresCar.OpenFuelCap().Wait();
                            break;
                        case 6:
                            featuresCar.CloseFuelCap().Wait();
                            break;
                        case 7:
                            int amount = AppReadLineInt("Enter amount of fuel to add: ", 1, 100);
                            featuresCar.Refuel(amount).Wait();
                            break;
                        case 8:
                            car.TogglePowerSwitch();
                            break;
                        case 9:
                            car.StartDriving();                 
                            break;
                        case 10:
                            car.StopDriving();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Unknown command.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Selected car does not support features.");
            }
        }

        public static CAR GenerateNewCarFromInt(int model)
        {
            return model switch
            {
                0 => new BENZ(new Engine("BENZ Engine")),
                1 => new BMW(new Engine("BMW Engine")),
                _ => throw new ArgumentException("Invalid car model.")
            };
        }

        public static int AppReadLineInt(string hint, int min, int max)
        {
            while (true)
            {
                Console.Write(hint);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine($"Please enter a valid number between {min} and {max}.");
            }
        }
    }
}