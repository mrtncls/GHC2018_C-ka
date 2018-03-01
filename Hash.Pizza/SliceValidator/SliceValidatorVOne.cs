using System;
using System.Collections.Generic;
using System.Text;

namespace Hash.Pizza.SliceValidator
{
    public class SliceValidatorVOne : ISliceValidator
    {
        public bool SliceIsValid(char[,] pizza, int xStart, int xEnd, int yStart, int yEnd, int lowestAmount, int highestAmount, int diffrentIndregients)
        {
            if ((xEnd-xStart+1) * (yEnd-yStart+1) > highestAmount) return false;

            var uniqueIngredients = new List<char>();

            for (int k = xStart; k <= xEnd; k++)
            {
                for (int l = yStart; l <= yEnd; l++)
                {
                    var val = pizza[k, l];

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
