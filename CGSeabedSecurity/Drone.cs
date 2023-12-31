using System;
using System.Collections.Generic;

namespace CGSeabedSecurity
{
    public class Drone
    {
        public int Id { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Emergency { get; set; } = 0;
        public int Battery { get; set; } = 0;
        public int StartX { get; set; } = 0;
        public string DroneAction { get; set; } = $"WAIT 1";
        public List<Radar> RadarInfo { get; set; } = new();
        public List<Creature> ScannedCreatures { get; set; } = new();

        public Drone(int id, int x, int y, int emergency, int battery)
        {
            Id = id;
            X = x;
            Y = y;
            Emergency = emergency;
            Battery = battery;
            StartX = X;
        }

        public void UpdateData(int x, int y, int emergency, int battery)
        {
            X = x;
            Y = y;
            Emergency = emergency;
            Battery = battery;

            if (Emergency == 1)
            {
                ScannedCreatures.Clear();
                Console.Error.WriteLine("SCARY!!!!");
            }

            if (Y == 0)
            {
                ScannedCreatures.Clear();
            }
        }

        public void ProcessRadar(Creature creature, string position)
        {
            var foundRadar = RadarInfo.Find(x => x.Creature.Id == creature.Id);
            if (foundRadar != null)
            {
                foundRadar.Position = position;
            }
            else
            {
                RadarInfo.Add(new Radar(creature, position));
            }
        }

        public void ProcessScans(Creature creature)
        {
            var foundCreature = ScannedCreatures.Find(x => x.Id == creature.Id);
            if (foundCreature == null)
            {
                ScannedCreatures.Add(creature);
            }
        }

        public bool IsLeft()
        {
            return X < 5000;
        }

        public override string ToString()
        {
            return $"Drone: Id={Id}, X={X}, Y={Y}, Emergency={Emergency}, Battery={Battery}";
        }
    }
}