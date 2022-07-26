namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync(); // Asenkton metodlar için
        void Commit();  // Asenkron olmayan metodlar için
    }
}
