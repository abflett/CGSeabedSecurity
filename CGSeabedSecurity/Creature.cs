using System.Collections.Generic;

namespace CGSeabedSecurity
{
    public class Creature
    {
        public enum CreatureColor { Pink, Yellow, Green, Blue };
        public enum CreatureType { Cephalopod, Fish, Crustacean };
        public int Id { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Vx { get; set; } = 0;
        public int Vy { get; set; } = 0;
        public bool WasVisible { get; set; } = false;
        public List<Drone> Drones { get; set; } = new List<Drone>();
        public CreatureColor Color { get; set; }
        public CreatureType Type { get; set; }

        public Creature(int id, int color, int type)
        {
            Id = id;
            Color = GetColorEnum(color);
            Type = GetTypeEnum(type);
        }

        private static CreatureColor GetColorEnum(int color)
        {
            return (CreatureColor)color;
        }

        private static CreatureType GetTypeEnum(int type)
        {
            return (CreatureType)type;
        }

        public override string ToString()
        {
            return $"CreatureId={Id}, X={X}, Y={Y}, Vx={Vx}, Vy={Vy}, Color={Color}, Type={Type}";
        }
    }
}