using async.Vechicle;
using async.Vechicle.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace async.Vechicle.Types
{
    public class BENZ : CAR, IFeaturesOfTheCar
    {
        Engine engine;

        public string Name => name;
        public BENZ(Engine engine) : base(engine)
        {
            this.engine = engine;
            base.CarLoop += OnLoop;
        }

        public Task OpenFuelDoor() {
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
            if (0 < base.fuelLevel && base.fuelLevel < 3)
            {
                LogStatus("warninig:BENZ Fuel level is low ==> Fuel :" + base.fuelLevel);
                LogStatus("Please Stop engine");
            }

            else if (base.fuelLevel == 0)
            {
                LogStatus("warning:BENZ There is not foul in your car .....!");
                engine.TurnOffEngine();
                IsDriving = false;
            }
            else
            {
                LogStatus("BENZ Fuel level is ok ==> Fuel : " + base.fuelLevel);
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
            if (IsDriving) {

                LogStatus("BENZ Current speed of your benz is :" + base.currentSpeed + "km/h");


                if (base.currentSpeed > 5)
                {
                    LogStatus("warning:BENZ Your speed is high please slow down your speed");
                }
                else if (base.currentSpeed > 10)
                {
                    LogStatus("warning:BENZ Your speed is in the maximum of its value ");
                }
                
            }

            return Task.CompletedTask;
        }

        public static BENZ BenzGClass()
        {
            return new BENZ(new Engine("G CLASS Engine Turbo"));
        }
        // call a super logic and child logic together
        public override void StartDriving()
        {
            if (engine.IsEngineRunning())
            {
                base.StartDriving();
                LogStatus("BENZ start driving");
            }
            else
            {
                LogStatus("Please first toggle power switch and start engine .... ");
            }
        }
        public override void StopDriving()
        {
            base.StopDriving();
            LogStatus("BENZ stop driving");
        }

        public override void TogglePowerSwitch()
        {
            if (base.IsDriving)
            {
                LogStatus("The BENZ cannot turn off the machine during the driving");
            }
            else
            {
                base.TogglePowerSwitch();
            }
        }
    }
}
