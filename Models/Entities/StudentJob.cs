namespace Skillora.Models.Entities
{
    public class StudentJob
    {
        public string StudentId { get; set; }
        public string JobId { get; set; }
        public Student Student { get; set; }
        public Job Job { get; set; }

        public bool applied { get; set; } = false; //true=not display

    }
}
