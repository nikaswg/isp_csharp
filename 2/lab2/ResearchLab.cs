namespace UniversitySystem
{
    // 14. Наследование обобщений
    public class ResearchLab<T> : Department where T : Professor
    {
        // 2. Конструктор с параметрами
        public ResearchLab(string labName, T head) : base(labName, "LAB-000")
        {
            LabName = labName;
            Head = head;
        }

        // 3. Свойства
        public string LabName { get; set; }
        public T Head { get; set; }
    }
}