using System;

namespace CGSeabedSecurity
{
    public class Game
    {
        private readonly CreatureManager _creatureManager;
        private readonly DroneManager _droneManager;
        private int _turn = 0;
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
                Console.Error.WriteLine($"Player: {_playerScore}, Enemy: {_enemyScore}");
                Console.Error.WriteLine("Turn: " + _turn);
                _turn++;
                ProcessData();
                _droneManager.Update(_turn);
                _droneManager.MakeActions();

            }
        }

        private void ProcessData()
        {
            _playerScore = Util.GetNumericValue();
            _enemyScore = Util.GetNumericValue();
            _creatureManager.ProcessScans();
            _droneManager.ProcessDrones();
            _droneManager.ProcessDroneScans();
            _creatureManager.ProcessVisibleCreatures(_turn);
            _droneManager.ProcessRadarBlips();

        }
    }
}