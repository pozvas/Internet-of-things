namespace WebApplication1
{
    static public class Thermometer
    {
        static public double GetTemp()
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * 50d + 10d, 1);
        }
    }
}
