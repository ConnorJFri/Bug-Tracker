namespace Bug_Tracker.Models
{
    public class BugTracker
    {

        public int ID { get; set; }
        public string Bug { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Asignee { get; set; }
        public BugTracker() 
        {
        
        }
    }
}
