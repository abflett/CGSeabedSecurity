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
        public bool PlayerDrone { get; set; } = false;

        public int TargetX { get; set; } = 0;
        public int TargetY { get; set; } = 0;
        public int Light { get; set; } = 0;
        public string Message { get; set; } = string.Empty;

        public bool ShouldSurface { get; set; } = false;

        public List<Radar> RadarInfo { get; set; } = new();
        public List<Creature> ScannedCreatures { get; set; } = new();


        public Drone(int id, int x, int y, int emergency, int battery, bool playerDrone)
        {
            Id = id;
            X = x;
            Y = y;
            Emergency = emergency;
            Battery = battery;
            StartX = X;
            PlayerDrone = playerDrone;
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
                Console.Error.WriteLine("MONSTER ATTACKED!");
            }

            if (Y == 0 && Emergency == 0)
            {
                Console.Error.WriteLine("SUBMITTED SCANS!");
                ScannedCreatures.Clear();
                ShouldSurface = false;
            }

            if (Y > 8000)
            {
                Console.Error.WriteLine("SHOULD SURFACE!");
                ShouldSurface = true;
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

        public void SetInitialDroneTarget()
        {
            TargetY = 8500;
            Light = 1;
            Message = "SEARCHING!";
            if (IsLeft())
            {
                TargetX = 2500;
            }
            else
            {
                TargetX = 7500;
            }


            if (ShouldSurface) // Surface if scanned more then 2 drones to submit scans.
            {
                TargetY = 0;
                Message = "SURFACING!";
            }
        }

        public void SetSafeTarget(List<Creature> monstersTooClose)
        {
            Console.Error.WriteLine(ToString());

            foreach (Creature monster in monstersTooClose)
            {
                Console.Error.WriteLine(monster.ToString());

                // dodge right
                if (X < monster.Nx)
                {
                    TargetX = monster.Nx - 600;
                    if (!ShouldSurface)
                    {
                        TargetY = Y + 600;
                    }
                    else
                    {
                        TargetY = Y - 600;
                    }
                }
                else
                {
                    TargetX = monster.Nx + 600;
                    if (!ShouldSurface)
                    {
                        TargetY = Y + 600;
                    }
                    else
                    {
                        TargetY = Y - 600;
                    }
                }
                Message = "AVOIDING MONSTER!";
            }
        }

        public override string ToString()
        {
            return $"Drone: Surface={ShouldSurface} Id={Id}, X={X}, Y={Y}, Emergency={Emergency}, Battery={Battery}";
        }
    }
}