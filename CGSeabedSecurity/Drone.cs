using System;
using System.Collections.Generic;
using System.Linq;

namespace CGSeabedSecurity
{
    public class Drone
    {
        public int Id { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Emergency { get; set; } = 0;
        public int Battery { get; set; } = 0;
        public List<Radar> RadarList { get; set; } = new();
        private int Sx = 0;
        private int Sy = 0;
        private bool isStarted = false;
        private Radar TargetRadaredCreature { get; set; }
        public string DroneAction { get; set; } = $"MOVE {5000} {5000} {1}";
        private bool _hasSurfaced = false;

        public Drone(int id, int x, int y, int emergency, int battery)
        {
            Id = id;
            X = x;
            Y = y;
            Emergency = emergency;
            Battery = battery;
        }

        public void UpdateRadar(Creature creature, string position)
        {
            var existingRadar = RadarList.Find(x => x.Creature == creature);
            if (existingRadar != null)
            {
                existingRadar.Position = position;
            }
            else
            {
                RadarList.Add(new Radar(creature, position));
            }
        }

        public override string ToString()
        {
            return $"Drone: Id={Id}, X={X}, Y={Y}, Emergency={Emergency}, Battery={Battery}";
        }

        public void Update(CreatureManager creatureManager)
        {
            SetDroneStart();
            var radarListOfAvailableCreatures = GetRadarOfAvailableCreatures(creatureManager);

            if (Sx < 5000)
            {
                DroneAction = $"MOVE {2500} {8000} {1}";
            }
            else
            {
                DroneAction = $"MOVE {7500} {8000} {1}";
            }

            Console.Error.WriteLine($"Player Avaiable {creatureManager.PlayerAvailableCreatures.Count}");

            if (Y < 1)
            {
                _hasSurfaced = true;
            }

            int count = creatureManager.Creatures.Count - 12;

            if ((creatureManager.PlayerAvailableCreatures.Count < count + 7 && !_hasSurfaced) || creatureManager.PlayerAvailableCreatures.Count < count + 1)
            {
                if (Sx < 5000)
                {
                    DroneAction = $"MOVE {2500} {0} {1}";
                }
                else
                {
                    DroneAction = $"MOVE {7500} {0} {1}";
                }
            }
        }

        private void SetDroneStart()
        {
            if (!isStarted)
            {
                Sx = X;
                Sy = Y;
                isStarted = true;
            }
        }

        private List<Radar> GetRadarOfAvailableCreatures(CreatureManager creatureManager)
        {
            var availableCreatures = creatureManager.PlayerAvailableCreatures;

            var radarListForAvailableCreatures = RadarList
                .Where(radar => availableCreatures.Contains(radar.Creature))
                .ToList();

            return radarListForAvailableCreatures;
        }
    }
}