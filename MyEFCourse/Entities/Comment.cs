namespace MyEFCourse.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public virtual User Author { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }
    }
}
