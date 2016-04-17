using System;
using System.Runtime.Serialization;

namespace jcTM.PCL.Transports {
    [DataContract]
    public class DayOverviewListingResponseItem {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime Day { get; set; }

        [DataMember]
        public double MinTemp { get; set; }

        [DataMember]
        public double MaxTemp { get; set; }

        [DataMember]
        public double AvgTemp { get; set; }
    }
}