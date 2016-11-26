namespace CalculatorApp.Models
{
    using System;
    using Shouldly;
    using Xunit;

    public class MathEquationTests
    {
        [Fact]
        public void NewCalculationShouldSetExpression()
        {
            var expression = "2+2";
            var sut = new MathEquation(expression);
            sut.Expression.ShouldBe(expression);
        }

        [Fact]
        public void NewCalculationShouldHaveNullResult()
        {
            var expression = "2+2";
            var sut = new MathEquation(expression);
            sut.Result.ShouldBeNull();
        }

        [Fact]
        public void BlankExpressionShouldHaveNullResult()
        {
            var expression = "";
            Should.Throw<ArgumentOutOfRangeException>(()=>new MathEquation(expression));
        }

        public void WhitespaceExpressionShouldHaveNullResult()
        {
            var expression = "    ";
            Should.Throw<ArgumentOutOfRangeException>(()=>new MathEquation(expression));
        }

        [Fact]
        public void NullExpressionShouldThrowException()
        {
            Should.Throw<ArgumentOutOfRangeException>(()=>new MathEquation(null));
        }
    }
}