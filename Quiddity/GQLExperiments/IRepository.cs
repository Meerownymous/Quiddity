namespace HotChocolatePlay
{
    public interface IRepository<T>
    {
        string Add(string title);
        ICollection<T> Find(string query);
    }
}