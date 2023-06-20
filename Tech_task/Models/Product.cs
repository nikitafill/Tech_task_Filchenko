using System.ComponentModel.DataAnnotations.Schema;

namespace Tech_task.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfProduction { get; set; }
        public Guid DepartmentId { get; set; } 

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
