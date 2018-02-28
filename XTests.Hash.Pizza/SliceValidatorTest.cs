using Hash.Pizza.SliceValidator;
using System;
using FluentAssertions;
using Xunit;

namespace Tests.Hash.Pizza
{

    public class SliceValidatorTests
    {


        [Fact]
        public void InValidSize()
        {
            ISliceValidator sliceValidator = new SliceValidatorVOne();

            var lowestAmount = 1;
            var highestAmount = 6;
            var diffrentIngredients = 2;

            sliceValidator.SliceIsValid(new char[4, 3], lowestAmount, highestAmount, diffrentIngredients).Should().Be(false);
            sliceValidator.SliceIsValid(new char[2, 6], lowestAmount, highestAmount, diffrentIngredients).Should().Be(false);
        }


        [Fact]
        public void ValidIngredients()
        {
            ISliceValidator sliceValidator = new SliceValidatorVOne();
            var lowestAmount = 1;
            var highestAmount = 6;
            var diffrentIngredients = 2;

            char[,] slice1 = new char[3, 2]
            {
                {'T' , 'T'},
                {'T' , 'M'},
                {'T' , 'T' }

            };

            sliceValidator.SliceIsValid(slice1, lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
        }

        [Fact]
        public void InValidIngredients()
        {
            ISliceValidator sliceValidator = new SliceValidatorVOne();
            var lowestAmount = 1;
            var highestAmount = 6;
            var diffrentIngredients = 2;

            char[,] slice1 = new char[3, 2]
            {
                {'T' , 'T'},
                {'T' , 'T'},
                {'T' , 'T' }

            };

            sliceValidator.SliceIsValid(slice1, lowestAmount, highestAmount, diffrentIngredients).Should().Be(false);
        }

    }
}
