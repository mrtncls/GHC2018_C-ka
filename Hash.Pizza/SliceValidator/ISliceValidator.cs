using System;
using System.Collections.Generic;
using System.Text;

namespace Hash.Pizza.SliceValidator
{
    public interface ISliceValidator
    {

        bool SliceIsValid(char[,] slice, int lowestAmount, int highestAmount, int diffrentIndregients);

    }
}
