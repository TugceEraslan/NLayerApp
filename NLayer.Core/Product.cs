using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Product: BaseEntity  // BaseEntity den miras aldık
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }  // Category-Product arasındaki bire-çok ilişki için CategoryId yi kullanmamız gerekecek
                                             // CategoryId si Product entity si için bir foreign key dir
        public Category Category { get; set; }// Her Product ın da bir tane Category si vardır. ona da Category ismini verdim
        public ProductFeature ProductFeature { get; set; }  // Product tablosunda da ProductFeature ı tutmam lazım 
    }
}
