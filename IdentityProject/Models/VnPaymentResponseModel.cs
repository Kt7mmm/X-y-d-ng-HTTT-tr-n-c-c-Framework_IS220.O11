namespace Cinema.Models
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }

        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }

    }
    public class VnPaymentRequestModel
    {

        public string cus_name { get; set; }
        public string bi_id { get; set; }
        public double total { get; set; }

        public DateTime bi_date { get; set; }



    }
}
