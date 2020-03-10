﻿using Newtonsoft.Json;
using NServiceBus;

namespace Core.NServiceBus
{
    [System.Serializable]
    public class Response :
        IMessage
    {
        public string Text{get;set;}
    }
}