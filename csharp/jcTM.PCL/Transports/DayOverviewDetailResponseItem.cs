using System.Runtime.Serialization;

namespace jcTM.PCL.Transports {
    [DataContract]
    public class DayOverviewDetailResponseItem {
        [DataMember]
        public int Hour { get; set; }

        [DataMember]
        public int AvgTemp { get; set; }
    }
}