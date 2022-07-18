using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ProductId { get; set; }     // ProductFeature da bir Product a ait olmak zorunda o yüzden bir de ProductId tutmalıyım
                                               // Burda ki foreign key ProductId olacak
        public Product Product { get; set; }   // Product nesnemizi tutalım
    }
}
