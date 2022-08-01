using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; } = new List<string>();  // Default listesini oluşturalım. NotFoundFilter.cs de eklemeye çalışırken null hatası almamak için yaparız bunu.
    }
}
