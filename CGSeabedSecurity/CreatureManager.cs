using System.Collections.Generic;

namespace CGSeabedSecurity
{
    public class CreatureManager
    {
        public List<Creature> GoodCreatures { get; set; } = new();
        public List<Creature> BadCreatures { get; set; } = new();

        public List<Creature> SubmittedPlayerCreatures { get; set; } = new();
        public List<Creature> SubmittedEnemyCreatures { get; set; } = new();

        public CreatureManager()
        {
            int creatureCount = Util.GetNumericValue();
            for (int i = 0; i < creatureCount; i++)
            {
                var data = Util.GetNumericValues();
                var creature = new Creature(data[0], data[1], data[2]);
                if (creature.Type == CreatureType.Monster)
                {
                    BadCreatures.Add(creature);
                }
                else
                {
                    GoodCreatures.Add(creature);
                }
            }
        }

        public Creature CreatureById(int id)
        {
            var goodCreature = GoodCreatures.Find(x => x.Id == id);

            if (goodCreature != null)
            {
                return goodCreature;
            }

            var badCreature = BadCreatures.Find(x => x.Id == id);
            return badCreature;
        }

        public Creature BadCreatureById(int id)
        {
            return BadCreatures.Find(x => x.Id == id);
        }

        public void ProcessScans()
        {
            UpdateSubmittedCreatures(SubmittedPlayerCreatures);
            UpdateSubmittedCreatures(SubmittedEnemyCreatures);
        }

        private void UpdateSubmittedCreatures(List<Creature> submittedCreatures)
        {
            int scanCount = Util.GetNumericValue();
            for (int i = 0; i < scanCount; i++)
            {
                int creatureId = Util.GetNumericValue();
                var foundSubmitted = submittedCreatures.Find(x => x.Id == creatureId);
                if (foundSubmitted == null)
                {
                    submittedCreatures.Add(CreatureById(creatureId));
                }
            }
        }

        public void ProcessVisibleCreatures()
        {
            int visibleCreatureCount = Util.GetNumericValue();
            for (int i = 0; i < visibleCreatureCount; i++)
            {
                var data = Util.GetNumericValues();
                var creature = CreatureById(data[0]);
                creature.UpdateData(data[1], data[2], data[3], data[4]);
            }
        }
    }
}