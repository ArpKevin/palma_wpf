using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pálma
{
    public class Dessert {
        public string DessertName { get; set; }
        public string DessertType { get; set; }
        public bool HasDessertBeenAwarded { get; set; }
        public int DessertPrice { get; set; }
        public string DessertUnit { get; set; }

        public Dessert(string dessertName, string dessertType, bool hasDessertBeenAwarded, int dessertPrice, string dessertUnit)
        {
            DessertName = dessertName;
            DessertType = dessertType;
            HasDessertBeenAwarded = hasDessertBeenAwarded;
            DessertPrice = dessertPrice;
            DessertUnit = dessertUnit;
        }

        public override string ToString()
        {
            return $"Desszert neve: {DessertName}, desszert típusa: {DessertType}, volt díjazott: {(HasDessertBeenAwarded ? "igen" : "nem")}, Desszert ára: {DessertPrice}, Desszert egysége: {DessertUnit}";
        }
    }   
}
