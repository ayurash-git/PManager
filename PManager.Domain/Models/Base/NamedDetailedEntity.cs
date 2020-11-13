namespace PManager.Domain.Models.Base
{
    public abstract class NamedDetailedEntity : NamedEntity
    {
        public string Details { get; set; }
    }
}