using System;

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

        public void Run()
        {
            while (true)
            {
                ProcessData();
                _droneManager.Update();
                _droneManager.MakeActions();
                Console.Error.WriteLine($"Player: {_playerScore}, Enemy: {_enemyScore}");
            }
        }

        private void ProcessData()
        {
            _playerScore = Util.GetNumericValue();
            _enemyScore = Util.GetNumericValue();
            _creatureManager.ProcessScans();
            _droneManager.ProcessDrones();
            _droneManager.ProcessDroneScans();
            _creatureManager.ProcessVisibleCreatures();
            _droneManager.ProcessRadarBlips();
        }
    }
}