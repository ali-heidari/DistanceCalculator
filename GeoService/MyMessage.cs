﻿using Newtonsoft.Json;
using NServiceBus;
 
[System.Serializable]
public class MyMessage :
    IMessage
{
    public string SomeProperty { get; set; }
}
 