using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model
{
    public class Sorting
    {
        public Sorting() { }

        public Sorting(string propertyName, bool isDescending = false)
        {
            PropertyName = propertyName;
            IsDescending = isDescending;
        }

        public string PropertyName { get; set; } = null!;

        public bool IsDescending { get; set; }
    }
}
