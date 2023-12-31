// Using dotnet-combine to merge multiple source files into one: https://github.com/eduherminio/dotnet-combine
// Example Command: dotnet-combine single-file C:\Dev\Source\CGSeabedSecurity\CGSeabedSecurity --output  C:\Dev\Source\CodingGame\SeabedSecurity.cs --overwrite true

namespace CGSeabedSecurity
{
    public class App
    {
        static void Main(string[] args)
        {
            CreatureManager creatureManager = new();
            DroneManager droneManager = new(creatureManager);

            Game game = new(creatureManager, droneManager);
            game.Run();
        }
    }
}