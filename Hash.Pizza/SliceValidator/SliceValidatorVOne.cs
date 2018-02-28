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

            return true;
        }
    }
}
