namespace PManager.Domain.Models.Base
{
    public abstract class NamedEntity : EntityInt
    {
        //[Required]
        public string Name { get; set; }
    }
}