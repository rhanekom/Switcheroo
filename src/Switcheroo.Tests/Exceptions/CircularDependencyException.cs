namespace Switcheroo.Tests.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException()
        {
        }

        public CircularDependencyException(string message) : base(message)
        {
        }

        public CircularDependencyException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CircularDependencyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }    
}
