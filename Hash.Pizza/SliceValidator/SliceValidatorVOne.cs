using System;
using System.Collections.Generic;
using System.Text;

namespace Hash.Pizza.SliceValidator
{
    public class SliceValidatorVOne : ISliceValidator
    {
        public bool SliceIsValid(char[,] slice, int lowestAmount, int highestAmount, int diffrentIndregients)
        {
            if (slice.GetLength(0) * slice.GetLength(1) > highestAmount) return false;

            var uniqueIngredients = new List<char>();

            for (int k = 0; k < slice.GetLength(0); k++)
            {
                for (int l = 0; l < slice.GetLength(1); l++)
                {
                    var val = slice[k, l];

                    if(!uniqueIngredients.Contains(val)) 
                        uniqueIngredients.Add(val);

                    if (uniqueIngredients.Count >= diffrentIndregients)
                        return true;
                }
            }
            
                

            return false;
        }
    }
}
