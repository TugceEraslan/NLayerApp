using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Exceptions
{
    public class ClientSideException: Exception   // Hata class ı olduğu için Exception class ından dan miras alıyorum
    {
        public ClientSideException(string message): base(message) // base dediğim Exception ın ctor una gidiyor
        {

        }
    }
}
