namespace MSIA.WebFresher032023.ConnectToDB.Middleware
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public string MoreInfo { get; set; }
        public string TraceId { get; set; }
    }
}
