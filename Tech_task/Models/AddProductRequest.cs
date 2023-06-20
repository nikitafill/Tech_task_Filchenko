namespace Tech_task.Models
{
    public class AddProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfProduction { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
