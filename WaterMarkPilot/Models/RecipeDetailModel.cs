using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterMarkPilot.Models
{
    public class RecipeDetailModel
    {
        public string DisplayName { get; set; }
        public string WalletName { get; set; }
        public string ReceiverName { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Coupon { get; set; }
        public string CouponCurrencyType { get; set; }
        public string RefCode { get; set; }
        public string RefDateTime { get; set; }
        public string OptionalDetail { get; set; }
        public string ProfileImgSenderUrl { get; set; }
        public string ProfileImgReceiverUrl { get; set; }

    }
}
