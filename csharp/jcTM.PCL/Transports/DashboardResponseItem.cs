using System;
using System.Runtime.Serialization;

namespace jcTM.PCL.Transports {
    [DataContract]
    public class DashboardResponseItem {
        [DataMember]
        public double Latest_Temperature { get; set; }

        [DataMember]
        public DateTime Latest_RecordedTime { get; set; }

        [DataMember]
        public double CurrentDay_HighTemperature { get; set; }

        [DataMember]
        public double CurrentDay_LowTemperature { get; set; }

        [DataMember]
        public double Latest_HumidityPercentage { get; set; }
    }
}