using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync(); // Asenkton metodlar için
        void Commit();  // Asenkron olmayan metodlar için
    }
}
