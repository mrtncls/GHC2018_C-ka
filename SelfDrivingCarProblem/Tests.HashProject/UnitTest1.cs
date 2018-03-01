using System;
using Xunit;

namespace Tests.HashProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var array = new int[,] {}
            var cars = 2;

            string result = string.Empty;

            Assert.Equal(result, @"1 0\n2 2 1");
        }
    }
}
