namespace Skillora.Models.Entities
{
    public class SelectedStudentJob
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string JobId { get; set; }
        public Job Job { get; set; }
        
        public bool Status { get; set; } //true=selected """"""  false=rejected
    }
}
