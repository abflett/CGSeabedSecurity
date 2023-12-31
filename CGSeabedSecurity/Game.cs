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

        private void ProcessGameLoop()
        {
            _playerScore = Util.GetNumericValue();
            _enemyScore = Util.GetNumericValue();
            _creatureManager.ProcessScans();
            _droneManager.ProcessDrones();
            _creatureManager.ProcessDroneScans(_droneManager);
            _creatureManager.ProcessVisibleCreatures();

            foreach (var creature in _creatureManager.Creatures)
            {
                Console.Error.WriteLine(creature.ToString());
            }

            _droneManager.ProcessRadarBlips();

            //foreach (var drone in _droneManager.PlayerDrones)
            //{
            //    Console.Error.WriteLine($"Player = DroneId: {drone.Id}, X: {drone.X}, Y: {drone.Y}");
            //    foreach (var radar in drone.RadarList)
            //    {
            //        Console.Error.WriteLine($"CreatureId: {radar.Creature.Id}, Position: {radar.Position}");
            //    }
            //}

            //foreach (var drone in _droneManager.EnemyDrones)
            //{
            //    Console.Error.WriteLine($"Enemy = DroneId: {drone.Id}, X: {drone.X}, Y: {drone.Y}");
            //    foreach (var radar in drone.RadarList)
            //    {
            //        Console.Error.WriteLine($"CreatureId: {radar.Creature.Id}, Position: {radar.Position}");
            //    }
            //}

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