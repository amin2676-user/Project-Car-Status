using async.Vechicle.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace async.Vechicle.Types
{
    internal class BMW : CAR, IFeaturesOfTheCar
    {
        Engine engine;
        public string Name => name;
        public BMW(Engine engine) : base(engine)
        {
            this.engine = engine;
            base.CarLoop += OnLoop;
        }

        public Task OpenFuelDoor()
        {
            base.StopTimer();
            return Task.CompletedTask;
        }

        public void OnLoop(object? sender, EventArgs e)
        {
            if (SpeedGuid)
            {
                ReduceSpeed();
            }
            else
            {
                IncreaseSpeed();
            }
            Task task1 = LugicCheckCurrentSpeed();
            Task task2 = LogicCheckFuel();



            CheckEngine();

        }
        public Task LogicCheckFuel()
        {
            if (base.fuelLevel != 0)
            {
                base.fuelLevel = base.fuelLevel - 1;
            }
            if (0 < base.fuelLevel && base.fuelLevel < 2)
            {
                Console.WriteLine("warninig:BMW Fuel level is low ==> Fuel :" + base.fuelLevel);
                Console.WriteLine("Please Stop engine");
            }

            else if (base.fuelLevel == 0)
            {
                Console.WriteLine("warning:BMW There is not foul in your car .....!");
                engine.TurnOffEngine();
                IsDriving = false;
            }
            else
            {
                Console.WriteLine("BMW Fuel level is ok ==> Fuel : " + base.fuelLevel);
            }

            return Task.CompletedTask;
        }
        public override Task ReduceSpeed()
        {
            base.ReduceSpeed();
            if (base.currentSpeed != 0)
            {
                base.currentSpeed = base.currentSpeed - 1;
            }
            else
            {

                base.StopDriving();

            }
            return Task.CompletedTask;
        }
        public override Task IncreaseSpeed()
        {
            base.IncreaseSpeed();

            base.currentSpeed = base.currentSpeed + 2;

            return Task.CompletedTask;
        }
        public Task LugicCheckCurrentSpeed()
        {
            if (IsDriving)
            {

                Console.WriteLine("BMW Current speed of your bmw is :" + base.currentSpeed + "km/h");


                if (base.currentSpeed > 8)
                {
                    Console.WriteLine("warning:BMW Your speed is high please slow down your speed");
                }
                else if (base.currentSpeed > 10)
                {
                    Console.WriteLine("warning:BMW Your speed is in the maximum of its value ");
                }

            }

            return Task.CompletedTask;
        }

        public static BMW Bmw()
        {
            return new BMW(new Engine("BMW Engine Turbo"));
        }
        // call a super logic and child logic together
        public override void StartDriving()
        {
            base.StartDriving();
            Console.WriteLine("BMW start driving");
        }
        public override void StopDriving()
        {
            base.StopDriving();
            Console.WriteLine("BMW stop driving");
        }

        public override void TogglePowerSwitch()
        {
            if (base.IsDriving)
            {
                Console.WriteLine("The BMW cannot turn off the machine during the driving");
            }
            else
            {
                base.TogglePowerSwitch();
            }
        }
    }
}
