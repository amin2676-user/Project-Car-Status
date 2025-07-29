using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async.Vechicle.Details
{
    public class Engine: IEngine
    {
        private bool IsRunning;
        private string name;

        public Engine(string name) {
            this.name = name;
        }
        public string Name => name;

        public bool IsEngineRunning()
        {
            return IsRunning;
        }

        public void StartEngine()
        {
            Console.WriteLine("Engine start running");
            IsRunning = true;
        }

        public void TurnOffEngine()
        {
            Console.WriteLine("Engine turned off");
            IsRunning = false;
        }

        public void UpgradeEngine()
        {
            Console.WriteLine("Engine");
        }
    }
}
