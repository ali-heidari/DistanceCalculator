﻿using Newtonsoft.Json;
using NServiceBus;
 
[System.Serializable]
public class GeoPoints :
    IMessage
{
    public float StartingLat { get; set; }
    public float StartingLng { get; set; }
    public float EndingLat { get; set; }
    public float EndingLng { get; set; }
}
 