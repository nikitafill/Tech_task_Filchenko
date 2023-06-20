namespace Tech_task.Models
{
    public class AddDepartmentRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public double Area { get; set; }
    }
}
