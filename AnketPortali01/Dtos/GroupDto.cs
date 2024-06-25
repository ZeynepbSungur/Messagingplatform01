namespace AnketPortali01.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public bool IsActive { get; internal set; }
        public object Name { get; internal set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
