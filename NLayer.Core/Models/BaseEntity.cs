namespace NLayer.Core.Models
{
    public abstract class BaseEntity   // abstract class BaseEntity olarak tanımladım ki BaseEntity den bir nesne örneği alınmasın
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
