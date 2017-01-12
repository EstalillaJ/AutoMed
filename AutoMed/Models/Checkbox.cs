using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{
    public class Checkbox<T>
    {
        public Checkbox()
        {

        }
        
        public Checkbox(T item)
        {
            this.Item = item;
        }
        public T Item { get; set; }
        public bool IsChecked { get; set; }
    }
}