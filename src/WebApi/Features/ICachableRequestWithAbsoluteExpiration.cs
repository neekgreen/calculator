namespace WebApi.Features
{
    using System;

    public interface ICachableRequestWithAbsoluteExpiration : ICachableRequest 
    {
        DateTimeOffset GetAbsoluteExpiration();
    }
}