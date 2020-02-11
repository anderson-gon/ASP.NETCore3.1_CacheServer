using System;

namespace CacheServer.Contract.Exceptions
{
    public class InvalidCacheKeyException : Exception
    {
        public InvalidCacheKeyException(string message) : base(message)
        {

        }
    }
}
