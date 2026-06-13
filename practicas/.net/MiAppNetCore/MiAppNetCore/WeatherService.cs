namespace MiAppNetCore;

public class WeatherService
{
    // CORRECCIÓN 1: eliminada variable privada sin usar (unusedVariable removida)

    // CORRECCIÓN 2: condición corregida, ya no siempre es true
    public static bool IsValidTemperature(int temp)
    {
        return temp is >= -50 and <= 60;
    }

    // CORRECCIÓN 3: método convertido a static
    public static string GetWeatherDescription(int temp)
    {
        if (temp < 0) return "Muy frio";
        if (temp < 10) return "Frio";
        if (temp < 20) return "Templado";
        if (temp < 30) return "Calido";
        return "Muy calido";
    }

    // CORRECCIÓN 4: método convertido a static
    public static string GetWeatherQuery(string city)
    {
        string query = "SELECT * FROM weather WHERE city = '" + city + "'";
        return query;
    }
}