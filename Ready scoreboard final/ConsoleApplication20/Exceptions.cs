using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class PlaneCrashedException : ApplicationException
    {
        public PlaneCrashedException() : base("Plane Crashed"){}
        public PlaneCrashedException(string message) : base(message) { }
        public PlaneCrashedException(String message, Exception ex) : base(message, ex) { }
        public PlaneCrashedException(SerializationInfo info, StreamingContext context):base(info, context){}
    }
    public class NotSuitableException : ApplicationException
    {
        public NotSuitableException() : base("You are not suitable to fly"){}
        public NotSuitableException(string message) : base(message) { }
        public NotSuitableException(String message, Exception ex) : base(message, ex) { }
        public NotSuitableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
