﻿using Newtonsoft.Json;
using NServiceBus;

namespace Core.NServiceBus
{
    [System.Serializable]
    public class GeoPoints :
        IMessage
    {
        public string UserGUID { get; set; }
        public float StartingLat { get; set; }
        public float StartingLng { get; set; }
        public float EndingLat { get; set; }
        public float EndingLng { get; set; }
    }
}