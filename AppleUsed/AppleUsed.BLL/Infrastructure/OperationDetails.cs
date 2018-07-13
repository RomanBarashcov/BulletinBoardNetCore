using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.BLL.Infrastructure
{
    public class OperationDetails<T>
    {
        public OperationDetails(bool succedeed, string message, T prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }

        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
        public T Property { get; private set; }
    }
}
