namespace DAL.Interface
{
    /// <summary>
    ///     Interface for entities with id
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///     Unique identifier
        /// </summary>
        int Id { get; }
    }
}
