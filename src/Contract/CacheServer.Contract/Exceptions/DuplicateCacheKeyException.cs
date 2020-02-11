using System;

namespace CacheServer.Contract.Exceptions
{
    public class DuplicateCacheKeyException : Exception
    {
        public DuplicateCacheKeyException(string message) : base(message)
        {

        }
    }
}
