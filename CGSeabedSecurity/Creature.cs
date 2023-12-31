namespace CGSeabedSecurity
{
    public enum CreatureColor { Red = -1, Pink, Yellow, Green, Blue };
    public enum CreatureType { Monster = -1, Cephalopod, Fish, Crustacean };

    public class Creature
    {

        public int Id { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Vx { get; set; } = 0;
        public int Vy { get; set; } = 0;

        public int Nx { get; set; } = 0;
        public int Ny { get; set; } = 0;
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

        public void UpdateData(int x, int y, int vx, int vy)
        {
            X = x;
            Y = y;
            Vx = vx;
            Vy = vy;
            Nx = X + Vx;
            Ny = Y + Vy;
        }

        public override string ToString()
        {
            return $"CreatureId={Id}, X={X}, Y={Y}, Vx={Vx}, Vy={Vy}, Color={Color}, Type={Type}";
        }
    }
}