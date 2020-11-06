using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class TransferDetails
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}