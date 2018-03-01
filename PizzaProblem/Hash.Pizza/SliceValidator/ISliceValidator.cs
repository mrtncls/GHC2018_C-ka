using System;
using System.Collections.Generic;
using System.Text;

namespace Hash.Pizza.SliceValidator
{
    public interface ISliceValidator
    {

        bool SliceIsValid(char[,] pizza, int xStart, int xEnd, int yStart, int yEnd, int lowestAmount, int highestAmount, int diffrentIndregients);

    }
}
