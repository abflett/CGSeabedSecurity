using System;
using System.Collections.Generic;
using System.Linq;

namespace CGSeabedSecurity
{
    public class CreatureManager
    {
        public List<Creature> Creatures { get; set; } = new List<Creature>();
        public List<Creature> PlayerDroneScannedCreatures { get; set; } = new List<Creature>();
        public List<Creature> PlayerSubmittedCreatures { get; set; } = new List<Creature>();
        public List<Creature> PlayerAvailableCreatures { get; set; } = new List<Creature>();
        public List<Creature> EnemyDroneScannedCreatures { get; set; } = new List<Creature>();
        public List<Creature> EnemySubmittedCreatures { get; set; } = new List<Creature>();
        public List<Creature> EnemyAvailableCreatures { get; set; } = new List<Creature>();

        public CreatureManager()
        {
            InitializeCreatures();
        }

        private void InitializeCreatures()
        {
            int creatureCount = Util.GetNumericValue();
            for (int i = 0; i < creatureCount; i++)
            {
                ReadCreatureData();
            }
        }

        private void ReadCreatureData()
        {
            var data = Util.GetNumericValues();
            int creatureId = data[0];
            int color = data[1];
            int type = data[2];
            var creature = new Creature(creatureId, color, type);

            Creatures.Add(creature);
            PlayerAvailableCreatures.Add(creature);
            EnemyAvailableCreatures.AddRange(Creatures);
        }

        public void ProcessDroneScans(DroneManager droneManager)
        {
            int droneScanCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < droneScanCount; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int droneId = int.Parse(inputs[0]);
                int creatureId = int.Parse(inputs[1]);
                Creature creature = Creatures.Find(x => x.Id == creatureId);

                UpdateDroneScannedCreatures(droneManager.PlayerDrones, PlayerDroneScannedCreatures, PlayerAvailableCreatures, creature, droneId);
                UpdateDroneScannedCreatures(droneManager.EnemyDrones, EnemyDroneScannedCreatures, EnemyAvailableCreatures, creature, droneId);
            }
        }

        private static void UpdateDroneScannedCreatures(List<Drone> drones, List<Creature> scannedCreatures, List<Creature> availableCreatures, Creature creature, int droneId)
        {
            foreach (var drone in drones)
            {
                var droneScannedCreature = scannedCreatures.FirstOrDefault(x => x.Id == creature.Id);

                if (droneId == drone.Id && droneScannedCreature == null)
                {
                    scannedCreatures.Add(creature);

                    var foundAvailableCreature = availableCreatures.FirstOrDefault(x => x.Id == creature.Id);
                    if (foundAvailableCreature != null)
                    {
                        availableCreatures.Remove(creature);
                    }
                }
            }
        }

        public void ProcessScans()
        {
            UpdateSubmittedScans(PlayerSubmittedCreatures);
            UpdateSubmittedScans(EnemySubmittedCreatures);
        }

        private void UpdateSubmittedScans(List<Creature> submittedCreatures)
        {
            int scanCount = Util.GetNumericValue();
            for (int i = 0; i < scanCount; i++)
            {
                int creatureId = Util.GetNumericValue();
                var creature = Creatures.Find(c => c.Id == creatureId);
                var submitcreature = submittedCreatures.Find(c => c.Id == creature.Id);
                if (submitcreature == null)
                {
                    submittedCreatures.Add(creature);
                }
            }
        }

        public void ProcessVisibleCreatures()
        {
            int visibleCreatureCount = Util.GetNumericValue();
            for (int i = 0; i < visibleCreatureCount; i++)
            {
                var data = Util.GetNumericValues();
                int creatureId = data[0];
                int creatureX = data[1];
                int creatureY = data[2];
                int creatureVx = data[3];
                int creatureVy = data[4];

                var creature = Creatures.Find(c => c.Id == creatureId);
                if (creature != null)
                {
                    creature.X = creatureX;
                    creature.Y = creatureY;
                    creature.Vx = creatureVx;
                    creature.Vy = creatureVy;
                    creature.WasVisible = true;
                }
            }
        }
    }
}