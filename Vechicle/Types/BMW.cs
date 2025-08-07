using async.Vechicle.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace async.Vechicle.Types
{
    public class BMW : CAR, IFeaturesOfTheCar
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
        public Task OpenFuelCap() => base.OpenFuelCap();
        public Task CloseFuelCap() => base.CloseFuelCap();
        public Task Refuel(int amount) => base.Refuel(amount);

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
                LogStatus("warninig:BMW Fuel level is low ==> Fuel :" + base.fuelLevel);
                LogStatus("Please Stop engine");
            }

            else if (base.fuelLevel == 0)
            {
                LogStatus("warning:BMW There is not foul in your car .....!");
                engine.TurnOffEngine();
                IsDriving = false;
            }
            else
            {
                LogStatus("BMW Fuel level is ok ==> Fuel : " + base.fuelLevel);
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
                StopDriving();
            }
            return Task.CompletedTask;
        }
        public override Task IncreaseSpeed()
        {
            if (!IsDriving)
            {
                LogStatus("You must start driving before accelerating (gas).");
                return Task.CompletedTask;
            }

            base.IncreaseSpeed();
            base.currentSpeed = base.currentSpeed + 1;
            return Task.CompletedTask;
        }
        public Task LugicCheckCurrentSpeed()
        {
            if (IsDriving)
            {

                LogStatus("BMW Current speed of your bmw is :" + base.currentSpeed + "km/h");


                if (base.currentSpeed > 8)
                {
                    LogStatus("warning:BMW Your speed is high please slow down your speed");
                }
                else if (base.currentSpeed > 10)
                {
                    LogStatus("warning:BMW Your speed is in the maximum of its value ");
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
            if (engine.IsEngineRunning())
            {
                base.StartDriving();
                LogStatus("BMW start driving");
            }
            else
            {
                LogStatus("Please first toggle power switch and start engine .... ");
            }
        }
        public override void StopDriving()
        {
            base.StopDriving();
            LogStatus("BMW stop driving");
        }

        public override void TogglePowerSwitch()
        {
            if (base.IsDriving)
            {
                LogStatus("The BMW cannot turn off the machine during the driving");
            }
            else
            {
                base.TogglePowerSwitch();
            }
        }

    }
}
