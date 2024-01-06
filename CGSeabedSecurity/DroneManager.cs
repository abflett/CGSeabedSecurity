using System;
using System.Collections.Generic;
using System.Linq;

namespace CGSeabedSecurity
{
    public class DroneManager
    {
        private readonly CreatureManager _creatureManager;
        public List<Drone> PlayerDrones { get; set; } = new();
        public List<Drone> EnemyDrones { get; set; } = new();

        public DroneManager(CreatureManager creatureManager)
        {
            _creatureManager = creatureManager;
        }

        public Drone DroneById(int id)
        {
            var playerDrone = PlayerDrones.Find(x => x.Id == id);

            if (playerDrone != null)
            {
                return playerDrone;
            }

            var enemyDrone = EnemyDrones.Find(x => x.Id == id);
            return enemyDrone;
        }

        public void ProcessDrones()
        {
            UpdateDrones(PlayerDrones, true);
            UpdateDrones(EnemyDrones, false);
        }

        private static void UpdateDrones(List<Drone> drones, bool playerDrone)
        {
            int droneCount = Util.GetNumericValue();
            for (int i = 0; i < droneCount; i++)
            {
                var data = Util.GetNumericValues();
                Drone drone = drones.Find(x => x.Id == data[0]);
                if (drone != null)
                {
                    drone.UpdateData(data[1], data[2], data[3], data[4]);
                }
                else
                {
                    drones.Add(new Drone(data[0], data[1], data[2], data[3], data[4], playerDrone));
                }
            }
        }

        public void ProcessRadarBlips()
        {
            int radarBlipCount = Util.GetNumericValue();
            for (int i = 0; i < radarBlipCount; i++)
            {
                var data = Console.ReadLine().Split(' ');
                Drone drone = DroneById(int.Parse(data[0]));
                Creature creature = _creatureManager.CreatureById(int.Parse(data[1]));
                drone.ProcessRadar(creature, data[2]);
            }
        }

        public void ProcessDroneScans()
        {
            int droneScanCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < droneScanCount; i++)
            {
                var data = Util.GetNumericValues();
                Drone drone = DroneById(data[0]);
                Creature creature = _creatureManager.CreatureById(data[1]);

                drone.ProcessScans(creature);
            }
        }

        public void Update(int turn)
        {
            RemoveSubmittedCreaturesFromDroneScans();
            foreach (var drone in PlayerDrones)
            {
                drone.SetInitialDroneTarget();
                var safetyDistance = 1200.00;
                var monstersTooClose = _creatureManager.BadCreatures.Where(x => x.UpdatedTurn == turn && safetyDistance > Util.CalculateDistance(drone.X, drone.Y, x.X, x.Y)).ToList();
                drone.SetSafeTarget(monstersTooClose);
            }
        }

        private void RemoveSubmittedCreaturesFromDroneScans()
        {
            foreach (var drone in PlayerDrones)
            {
                foreach (var creature in _creatureManager.SubmittedPlayerCreatures)
                {
                    drone.ScannedCreatures.Remove(creature);
                }
            }
        }

        public void MakeActions()
        {
            foreach (var drone in PlayerDrones)
            {
                Console.WriteLine($"MOVE {drone.TargetX} {drone.TargetY} {drone.Light} {drone.Message}");
            }
        }
    }
}