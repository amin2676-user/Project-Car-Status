using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
using async;
using async.Vechicle.Details;
using async.Vechicle.Types;
using async.Vechicle;
using System.Security.Cryptography.X509Certificates;

namespace mainNameSpace
{
    // Main Program
    class Program
    {

        public static (int Sum, int Product, int A) Calculate()
        {
            TestInternal();
            void TestInternal() { 
            
            }
            return (0, 2, 3);
        }
        public static Singleton_Instance_Best_way class1 = new Singleton_Instance_Best_way();

        static void Main(string[] args)
        {
            Calculate().Test
            Singleton_Instance_Best_way.GetSignelInstance(Singleton_Instance_Best_way.Class1Type.LocalDataBase);
            Console.WriteLine("Please En");
            while (true) {

                    BENZ benz = BENZ.BenzGClass();
                    if (benz is BENZ)
                    {
                        //start
                        benz.TogglePowerSwitch();
                        benz.StartDriving();
                        Thread.Sleep(5000);
                        benz.ReduceSpeed();
                        Thread.Sleep(5000);
                        benz.TogglePowerSwitch();

                        Thread.Sleep(15000);
                        //stop
                        benz.TogglePowerSwitch();
                        Thread.Sleep(10000);


                        benz.OpenFuelDoor();
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("");
            }

        }
    }

}



       