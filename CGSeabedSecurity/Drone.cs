namespace CGSeabedSecurity
{
    public class Drone
    {
        public int Id { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Emergency { get; set; } = 0;
        public int Battery { get; set; } = 0;

        public Drone(int id, int x, int y, int emergency, int battery)
        {
            Id = id;
            X = x;
            Y = y;
            Emergency = emergency;
            Battery = battery;
        }

        public override string ToString()
        {
            return $"Drone: Id={Id}, X={X}, Y={Y}, Emergency={Emergency}, Battery={Battery}";
        }
    }
}