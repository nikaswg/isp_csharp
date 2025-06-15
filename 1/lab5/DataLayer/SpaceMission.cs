public class SpaceMission
{
    public string Name { get; set; }         // Название миссии
    public string Target { get; set; }       // Тип цели (соответствует Type из CelestialBody)
    public string Agency { get; set; }       // Космическое агентство
    public int Year { get; set; }            // Год запуска
}