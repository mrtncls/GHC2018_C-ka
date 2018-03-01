using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hash.Pizza.SliceValidator
{
    public class SliceValidatorVOne : ISliceValidator
    {
        public bool SliceIsValid(char[,] pizza, int xStart, int xEnd, int yStart, int yEnd, int lowestAmount, int highestAmount, int diffrentIndregients)
        {
            if ((xEnd-xStart+1) * (yEnd-yStart+1) > highestAmount) return false;

            var ingredients = new List<char>();

            for (int k = xStart; k <= xEnd; k++)
            {
                for (int l = yStart; l <= yEnd; l++)
                {

                        ingredients.Add(pizza[k, l]);

                    if (ingredients.Count(x => x == 'T') >= lowestAmount && ingredients.Count(x => x == 'M') >= lowestAmount)
                        return true;
                }
            }
            
                

            return false;
        }
    }
}
