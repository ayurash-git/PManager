namespace PManager.Domain.Models.Base
{
    public abstract class NamedEntity : Entity
    {
        //[Required]
        public string Name { get; set; }
    }
}