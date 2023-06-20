namespace Tech_task.Models
{
    public class UpdateWorkerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Work { get; set; }
        public long Phone { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
