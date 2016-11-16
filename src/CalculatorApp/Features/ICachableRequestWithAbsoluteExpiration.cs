namespace CalculatorApp.Features
{
    using System;

    public interface ICachableRequestWithAbsoluteExpiration : ICachableRequest 
    {
        DateTimeOffset GetAbsoluteExpiration();
    }
}