namespace CGSeabedSecurity
{
    public class Game
    {
        private readonly CreatureManager _creatureManager;
        private readonly DroneManager _droneManager;
        private int _playerScore = 0;
        private int _enemyScore = 0;

        public Game(CreatureManager creatureManager, DroneManager droneManager)
        {
            _creatureManager = creatureManager;
            _droneManager = droneManager;
        }

        private void ProcessGameLoop()
        {
            _playerScore = Util.GetNumericValue();
            _enemyScore = Util.GetNumericValue();
            _creatureManager.ProcessScans();
            _droneManager.ProcessDrones();
            _creatureManager.ProcessDroneScans(_droneManager);
            _creatureManager.ProcessVisibleCreatures();
            _droneManager.ProcessRadarBlips();
            _droneManager.DroneActions();
        }

        public void Run()
        {
            while (true)
            {
                ProcessGameLoop();
            }
        }
    }
}