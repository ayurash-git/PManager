using PManager.Interfaces;

namespace PManager.Domain.Models.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
