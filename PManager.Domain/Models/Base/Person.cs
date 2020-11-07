namespace PManager.Domain.Models.Base
{
    public abstract class Person : EntityInt
    {
        //[Required]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}