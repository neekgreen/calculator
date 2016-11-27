namespace WebApi.Features
{
    using System;

    public interface ICachableRequestWithSlidingExpiration : ICachableRequest
    {
        TimeSpan GetSlidingExpiration();
    }
}