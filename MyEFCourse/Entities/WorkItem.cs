using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEFCourse.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string Area { get; set; }
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        //Epic
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        //Issue
        public double Effort { get; set; }
        //Task
        public string Activity { get; set; }
        public double RemainingWork { get; set; }

        public string Type { get; set; }
    }
}
