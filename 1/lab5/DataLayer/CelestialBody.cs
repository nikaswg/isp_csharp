public class CelestialBody
{
    public string Name { get; set; }         // Название объекта
    public int DiscoveryYear { get; set; }   // Год открытия (может быть отрицательным для древних объектов)
    public string Type { get; set; }         // Тип объекта
    public double Mass { get; set; }         // Масса в массах Земли
}