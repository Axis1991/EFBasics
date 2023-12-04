using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEFCourse.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        [Required]
        public string State { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Area { get; set; }
        [Column("My_iteration_name")]
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        //Epic
        public DateTime? StartDate { get; set; }
        [Precision(3)]
        public DateTime? EndDate { get; set;}
        //Issue
        [Column(TypeName = "double(5,2)")]
        public double Effort { get; set; }
        //Task
        [MaxLength(100)]
        public string Activity { get; set; }
        [Precision(14,2)]
        public double RemainingWork { get; set; }

        public string Type { get; set; }
    }
}
