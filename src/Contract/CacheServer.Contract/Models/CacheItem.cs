﻿using System;

namespace CacheServer.Contract.Models
{
    public class CacheItem
    {
        public string Key { get; set; }
        public string Value { get; set; }  
        
        public bool IsValidKey()
        {
            return !string.IsNullOrEmpty(Key);
        }
    }
}
