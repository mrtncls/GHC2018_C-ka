﻿using Hash.Pizza.SliceValidator;
using System;
using FluentAssertions;
using Xunit;

namespace Tests.Hash.Pizza
{

    public class SliceValidatorTests
    {

        [Fact]
        public void ValidSize()
        {
            ISliceValidator  sliceValidator = new SliceValidatorVOne();
            var lowestAmount = 1;
            var highestAmount = 6;
            var diffrentIngredients = 2;

            sliceValidator.SliceIsValid(new char[1,6], lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
            sliceValidator.SliceIsValid(new char[2,3], lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
            sliceValidator.SliceIsValid(new char[2,2], lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
            sliceValidator.SliceIsValid(new char[1,4], lowestAmount, highestAmount, diffrentIngredients).Should().Be(true);
        }

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


    }
}
