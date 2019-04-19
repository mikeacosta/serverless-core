using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ServerlessCore.Data.Models
{
    public class CollectionDto<T> : INotifyCompletion
    {
        public IList<T> Items { get; set; } = new List<T>();

        public CollectionDto<T> GetAwaiter()
        {
            return this;
        }

        public CollectionDto<T> GetResult()
        {
            return this;
        }

        public bool IsCompleted
        {
            get { return true; }
        }

        public void OnCompleted(Action continuation)
        {
        }
    }
}
