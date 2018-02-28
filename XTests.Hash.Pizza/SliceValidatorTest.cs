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

            sliceValidator.SliceIsValid(new char[4, 3], 0, 3, 0, 2, lowestAmount, highestAmount, diffrentIngredients)
                .Should().Be(false);
            sliceValidator.SliceIsValid(new char[2, 6], 0, 1, 0, 5, lowestAmount, highestAmount, diffrentIngredients)
                .Should().Be(false);
        }


        [Fact]
        public void ValidIngredients()
        {
            ISliceValidator sliceValidator = new SliceValidatorVOne();
            var lowestAmount = 1;
            var highestAmount = 6;
            var diffrentIngredients = 2;

            char[,] pizza = new char[3, 5]
            {
                {'T', 'T', 'T', 'T', 'T'},
                {'T', 'M', 'M', 'M', 'T'},
                {'T', 'T', 'T', 'T', 'T'}
            };

            sliceValidator.SliceIsValid(pizza, 0, 2, 0, 1, lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
            sliceValidator.SliceIsValid(pizza, 0, 2, 3, 4, lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
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
                {'T', 'T'},
                {'T', 'T'},
                {'T', 'T'}
            };

            sliceValidator.SliceIsValid(slice1, 0, 2, 0, 1, lowestAmount, highestAmount, diffrentIngredients).Should()
                .Be(false);
        }
    }
}