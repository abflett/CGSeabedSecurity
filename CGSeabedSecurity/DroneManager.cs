using System;
using System.Collections.Generic;

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
            UpdateDrones(PlayerDrones);
            UpdateDrones(EnemyDrones);
        }

        private static void UpdateDrones(List<Drone> drones)
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
                    drones.Add(new Drone(data[0], data[1], data[2], data[3], data[4]));
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

        public void DroneActions()
        {
            for (int i = 0; i < PlayerDrones.Count; i++)
            {
                Console.WriteLine(PlayerDrones[i].DroneAction);
            }
        }

        public void Update()
        {
            foreach (var drone in PlayerDrones)
            {
                foreach (var creature in _creatureManager.SubmittedPlayerCreatures)
                {
                    drone.ScannedCreatures.Remove(creature);
                }


                if (drone.IsLeft())
                {
                    drone.DroneAction = $"MOVE {2500} {8500} {1}";
                }
                else
                {
                    drone.DroneAction = $"MOVE {7500} {8500} {1}";
                }

                if (drone.ScannedCreatures.Count > 2)
                {
                    if (drone.IsLeft())
                    {
                        drone.DroneAction = $"MOVE {2500} {0} {1}";
                    }
                    else
                    {
                        drone.DroneAction = $"MOVE {7500} {0} {1}";
                    }
                }

                foreach (var creature in _creatureManager.BadCreatures)
                {
                    int avoidanceDistance = 1500;
                    var currentDistanceFromDrone = Util.CalculateDistance(drone.X, drone.Y, creature.X, creature.Y);

                    // Check if the drone is too close to a bad creature
                    if (currentDistanceFromDrone < 1000.00)
                    {
                        // Calculate a new position away from the bad creature
                        int newX = drone.X + creature.Vx * -1 * avoidanceDistance;
                        int newY = drone.Y + creature.Vy * -1 * avoidanceDistance;

                        // Update the drone's position and action
                        drone.DroneAction = $"MOVE {newX} {newY} {1} RUNNING AWAY!!!";
                    }
                }
            }
        }

        public void MakeActions()
        {
            foreach (var drone in PlayerDrones)
            {
                Console.WriteLine(drone.DroneAction);
            }
        }
    }
}