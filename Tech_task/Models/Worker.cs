using System.ComponentModel.DataAnnotations.Schema;
using Tech_task.Models;

namespace Tech_task.Models
{
    public class Worker
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Work { get; set; }
        public long Phone { get; set; }

        public Guid DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]

        public Department Department { get; set; }
    }
}