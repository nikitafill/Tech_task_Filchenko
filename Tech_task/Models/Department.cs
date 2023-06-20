namespace Tech_task.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public double Area { get; set; }

        public ICollection<Worker> Worker { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
