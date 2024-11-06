﻿namespace EventsWebApp.Domain.Exceptions
{
    public class ServiceNotAvailableException : Exception
    {
        public ServiceNotAvailableException(string message) : base(message)
        {
        }
    }
}