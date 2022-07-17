using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Category: BaseEntity  // BaseEntity den miras alsın CreatedDate ve UpdatedDate içerisinde zaten gelmiş olacak
    {
        public string Name { get; set; }
        public ICollection<Product> Products  { get; set; }  // ICollection<Product> ile tanımını yaptım ki
                                                             // artık Category nin birden fazla Products ı olabilecek
    }
}
