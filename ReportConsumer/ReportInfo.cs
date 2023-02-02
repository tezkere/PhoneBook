using Newtonsoft.Json;

namespace ReportConsumer
{
    public class ReportInfo
    {
        [JsonProperty("locationName")]
        public string LocationName { get; set; }
        [JsonProperty("contactCount")]
        public int ContactCount { get; set; }
        [JsonProperty("phoneCount")]
        public int PhoneCount { get; set; }
        [JsonProperty("reportId")]
        public Guid ReportId { get; set; }
    }
}
