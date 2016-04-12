using System;
using System.Runtime.Serialization;

namespace jcTM.PCL.Transports {
    [DataContract]
    public class LatestTemperatureResponseItem {
        [DataMember]
        public double Temperature { get; set; }

        [DataMember]
        public DateTime RecordedTime { get; set; }
    }
}