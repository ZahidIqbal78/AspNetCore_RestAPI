namespace AspNetCore_RestAPI.Models
{
    /// <summary>
    /// This model is for demonstration only, not a domain or database model.
    /// Domain or database models will be added to the dbContext and will
    /// be a child class of BaseModel.
    /// </summary>
    public class Post
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}