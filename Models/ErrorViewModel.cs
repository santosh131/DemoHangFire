using System;

namespace DemoHangFire.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
