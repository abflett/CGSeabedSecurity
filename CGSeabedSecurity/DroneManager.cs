using System;
using System.Collections.Generic;
using System.Linq;

namespace CGSeabedSecurity
{
    public class DroneManager
    {
        private readonly CreatureManager _creatureManager;

        public List<Drone> PlayerDrones { get; set; } = new List<Drone>();
        public List<Drone> EnemyDrones { get; set; } = new List<Drone>();

        public DroneManager(CreatureManager creatureManager)
        {
            _creatureManager = creatureManager;
        }

        public void ProcessDrones()
        {
            PlayerDrones = ReadDrones(PlayerDrones);
            EnemyDrones = ReadDrones(EnemyDrones);
        }

        private static List<Drone> ReadDrones(List<Drone> existingDrones)
        {
            int droneCount = Util.GetNumericValue();
            List<Drone> drones = new();

            for (int i = 0; i < droneCount; i++)
            {
                var data = Util.GetNumericValues();
                int droneId = data[0];
                int droneX = data[1];
                int droneY = data[2];
                int emergency = data[3];
                int battery = data[4];

                Drone existingDrone = existingDrones.Find(d => d.Id == droneId);

                if (existingDrone != null)
                {
                    existingDrone.X = droneX;
                    existingDrone.Y = droneY;
                    existingDrone.Emergency = emergency;
                    existingDrone.Battery = battery;
                    drones.Add(existingDrone);
                }
                else
                {
                    drones.Add(new Drone(droneId, droneX, droneY, emergency, battery));
                }
            }

            return drones;
        }

        public void ProcessRadarBlips()
        {
            int radarBlipCount = Util.GetNumericValue();
            for (int i = 0; i < radarBlipCount; i++)
            {
                var data = Console.ReadLine().Split(' ');
                int droneId = int.Parse(data[0]);
                int creatureId = int.Parse(data[1]);
                string radar = data[2];

                var creature = _creatureManager.Creatures.Find(x => x.Id == creatureId);
                var playerDrone = PlayerDrones.FirstOrDefault(x => x.Id == droneId);
                var enemyDrone = EnemyDrones.FirstOrDefault(x => x.Id == droneId);

                if (playerDrone != null)
                {
                    playerDrone.UpdateRadar(creature, radar);
                }
                else
                {
                    enemyDrone.UpdateRadar(creature, radar);
                }
            }
        }

        public void DroneActions()
        {
            for (int i = 0; i < PlayerDrones.Count; i++)
            {
                PlayerDrones[i].Update(_creatureManager);
            }

            for (int i = 0; i < PlayerDrones.Count; i++)
            {
                Console.WriteLine(PlayerDrones[i].DroneAction);
            }
        }
    }
}