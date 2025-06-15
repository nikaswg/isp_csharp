public class Expedition
{
    public string Name { get; set; }         // Название экспедиции
    public string TargetCulture { get; set; }// Целевая культура (соответствует Culture из Artifact)
    public string Leader { get; set; }       // Руководитель экспедиции
    public int Year { get; set; }            // Год экспедиции
}