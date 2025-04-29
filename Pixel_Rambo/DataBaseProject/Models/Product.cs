using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class Product // מחלקה שמייצגת מוצרים בחנות המשחק
    {
        public int Id { get; set; } = 1; // מזהה ייחודי של המוצר
        public int Price { get; set; } = 1; // מחיר המוצר
    }
}
