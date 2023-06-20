namespace Tech_task.Models
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfProduction { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
