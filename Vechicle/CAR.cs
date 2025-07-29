using async.Vechicle.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace async.Vechicle;
public abstract class CAR 
{
    Engine engine;
    public CAR(Engine engine)
    {
        this.engine = engine;
        aTimer.Elapsed += OnTimedEvent;
        aTimer.Enabled = true;
    }
    public bool IsDriving;
    public int currentSpeed = 0;
    public int fuelLevel = 5;
    public bool SpeedGuid;
    public string name;

    //timers have built in Task and it run the callback function async
    private static System.Timers.Timer aTimer = new System.Timers.Timer(2000);

    public virtual void StartDriving()
    {
        Console.WriteLine("Car StartDriving");
        IsDriving = true;
    }
    public virtual void StopDriving()
    {
        Console.WriteLine("Car StopDriving");
        IsDriving = false;
    }
    public virtual void TogglePowerSwitch()
    {
        Console.WriteLine("Car TogglePowerSwitch");

        if (engine.IsEngineRunning())
        {
            engine.TurnOffEngine();
            aTimer.Stop();
        }
        else
        {
            engine.StartEngine();
            aTimer.Start();

        }


    }
    public virtual Task ReduceSpeed()
    {
        SpeedGuid = true;
        return Task.CompletedTask;
    }
    public virtual Task IncreaseSpeed()
    {
        SpeedGuid = false;
        return Task.CompletedTask;
    }
    public virtual void CheckEngine()
    {
        if (engine.IsEngineRunning() == false)
        {
            currentSpeed = 0;
            aTimer.Stop();
        }
    }


    public void StopTimer() {
        aTimer.Stop();
    }

    //callback pattern method
    private void OnTimedEvent(Object? source, ElapsedEventArgs e)
    {
        CarLoop("Looop", EventArgs.Empty);
    }

    public event EventHandler CarLoop;

    public bool IsCarHasAutoPoilet;
}
