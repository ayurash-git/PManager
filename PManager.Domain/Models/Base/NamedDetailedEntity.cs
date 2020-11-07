namespace PManager.Domain.Models.Base
{
    public abstract class NamedDetailedEntity : NamedEntity
    {
        //[Required]
        public string Details { get; set; }
    }
}