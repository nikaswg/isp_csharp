namespace DataLayer
{
    public class Composer
    {
        public string Name { get; set; }       // Имя композитора
        public string Era { get; set; }         // Эпоха (Барокко, Классицизм и т.д.)
        public string FavoriteInstrument { get; set; }  // Любимый инструмент (соответствует Type из MusicalInstrument)
    }
}