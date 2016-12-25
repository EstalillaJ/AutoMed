using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{
    public class Checkbox<T>
    {
        public T Item { get; set; }
        public bool IsChecked { get; set; }
    }
}