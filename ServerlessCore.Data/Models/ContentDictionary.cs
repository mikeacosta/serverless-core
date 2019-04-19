using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ServerlessCore.Data.Models
{
    public class ContentDictionary : Dictionary<string, string>, INotifyCompletion
    {
        public ContentDictionary GetAwaiter()
        {
            return this;
        }

        public ContentDictionary GetResult()
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
