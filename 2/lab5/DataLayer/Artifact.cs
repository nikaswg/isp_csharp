public class Artifact
{
    public string Name { get; set; }         // Название артефакта
    public int DiscoveryYear { get; set; }   // Год находки (отрицательный для дат до н.э.)
    public string Culture { get; set; }      // Культура/цивилизация
    public decimal Value { get; set; }       // Оценочная стоимость в USD
}