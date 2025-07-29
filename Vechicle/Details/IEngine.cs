namespace async.Vechicle.Details
{
    interface IEngine
    {
        string Name { get; }
        public void StartEngine();
        public void TurnOffEngine();
        public void UpgradeEngine();

        public bool IsEngineRunning();
        

    }
}
