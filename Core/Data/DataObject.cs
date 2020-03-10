
using System;

namespace Core.Data
{
    public abstract class DataObject
    {
        public abstract string DocumentName { get; }
        public Guid guid{get;set;}
    }
}