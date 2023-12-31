namespace CGSeabedSecurity
{
    public class Radar
    {
        public Creature Creature { get; set; }
        public string Position { get; set; } = string.Empty;

        public Radar(Creature creature, string position)
        {
            Creature = creature;
            Position = position;
        }

        public override string ToString()
        {
            return $"CreatureId = {Creature.Id}, Position = {Position}";
        }
    }
}
