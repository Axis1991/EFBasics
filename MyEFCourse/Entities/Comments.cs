namespace MyEFCourse.Entities
{
    public class Comments
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
