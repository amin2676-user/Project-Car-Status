using async.Vechicle.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Text.Json;
using System.IO;

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
    public bool SpeedGuid;
    public string name;
    public bool IsFuelCapOpen { get; private set; } = false;
    public int fuelLevel = 5;

    //timers have built in Task and it run the callback function async
    private static System.Timers.Timer aTimer = new System.Timers.Timer(10000);

    public virtual void StartDriving()
    {
        //LogStatus("Car StartDriving");
        IsDriving = true;
    }
    public virtual void StopDriving()
    {
        //LogStatus("Car StopDriving");
        IsDriving = false;
    }
    public virtual void TogglePowerSwitch()
    {
        LogStatus("Car TogglePowerSwitch");

        if (engine.IsEngineRunning())
        {
            engine.TurnOffEngine();
            aTimer.Stop();
        }
        else
        {
            if (IsFuelCapOpen)
            {
                LogStatus("Cannot start engine while fuel cap is open. Please close the fuel cap first.");
                return;
            }
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

    public virtual Task OpenFuelCap()
    {
        if (engine.IsEngineRunning())
        {
            LogStatus("Your car engine is turn on please turn off the engine for open fuel cap .");
        }
        else if (!IsFuelCapOpen)
        {
            IsFuelCapOpen = true;
            LogStatus("Fuel cap opened.");
        }
        else
        {
            LogStatus("Fuel cap is already open.");
        }
        return Task.CompletedTask;
    }

    public virtual Task CloseFuelCap()
    {
        if (IsFuelCapOpen)
        {
            IsFuelCapOpen = false;
            LogStatus("Fuel cap closed.");
        }
        else
        {
            LogStatus("Fuel cap is already closed.");
        }
        return Task.CompletedTask;
    }

    public virtual Task Refuel()
    {
        if (!IsFuelCapOpen)
        {
            LogStatus("Cannot refuel: fuel cap is closed. Please open the fuel cap first.");
        }
        else
        {
            fuelLevel += 5; // Example logic
            Console.WriteLine($"Car refueled. Current fuel level: {fuelLevel}");
        }
        return Task.CompletedTask;
    }

    public virtual Task Refuel(int amount)
    {
        if (!IsFuelCapOpen)
        {
            LogStatus("Cannot refuel: fuel cap is closed. Please open the fuel cap first.");
        }
        else if (amount <= 0)
        {
            LogStatus("Amount must be greater than zero.");
        }
        else
        {
            fuelLevel += amount;
            Console.WriteLine($"Car refueled by {amount}. Current fuel level: {fuelLevel}");
        }
        return Task.CompletedTask;
    }

    private static readonly string StatusFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "status.json"
    );
    private static readonly object fileLock = new();

    protected void LogStatus(string message)
    {
        var status = new
        {
            Time = DateTime.Now,
            Message = message,
            Speed = currentSpeed,
            Fuel = fuelLevel,
            Engine = engine.IsEngineRunning(),
            IsDriving = IsDriving,
            IsFuelCapOpen = IsFuelCapOpen
        };

        lock (fileLock)
        {
            List<object> logs;
            if (File.Exists(StatusFilePath))
            {
                var json = File.ReadAllText(StatusFilePath);
                logs = JsonSerializer.Deserialize<List<object>>(json) ?? new List<object>();
            }
            else
            {
                logs = new List<object>();
            }
            logs.Add(status);
            var newJson = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StatusFilePath, newJson);
        }
    }
}
