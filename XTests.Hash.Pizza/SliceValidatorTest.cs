using Hash.Pizza.SliceValidator;
using System;
using Xunit;

namespace Tests.Hash.Pizza
{

    public class SliceValidatorTests
    {
        [Fact]
        public void TestSliceValidatorVOne()
        {
            ISliceValidator  sliceValidator = new SliceValidatorVOne();
        }
    }
}
