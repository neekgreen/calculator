namespace CalculatorApp.Models
{
    using System;
    using Shouldly;
    using Xunit;

    public class CalculatorTests
    {
        [Fact]
        public void NewCalculationShouldSetExpression()
        {
            var expression = "2+2";
            var calculation = new Calculation(expression);
            calculation.Expression.ShouldBe(expression);
        }

        [Fact]
        public void NewCalculationShouldHaveNullResult()
        {
            var expression = "2+2";
            var calculation = new Calculation(expression);
            calculation.Result.ShouldBeNull();
        }

        [Fact]
        public void BlankExpressionShouldHaveNullResult()
        {
            var expression = "";
            Should.Throw<ArgumentOutOfRangeException>(()=>new Calculation(expression));
        }

        public void WhitespaceExpressionShouldHaveNullResult()
        {
            var expression = "    ";
            Should.Throw<ArgumentOutOfRangeException>(()=>new Calculation(expression));
        }

        [Fact]
        public void NullExpressionShouldThrowException()
        {
            Should.Throw<ArgumentOutOfRangeException>(()=>new Calculation(null));
        }
    }
}