using System;

namespace CacheServer.Contract.Exceptions
{
    public class CacheKeyNotFoundException : Exception
    {
        public CacheKeyNotFoundException(string message) : base(message)
        {

        }
    }
}
