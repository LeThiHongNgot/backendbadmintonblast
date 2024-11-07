using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BadmintonBlast.Helpers;
using BadmintonBlast.RequestModels;
using System;
using System.Threading.Tasks;
using System.Web;

namespace BadmintonBlast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNpayController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;
        private readonly string _returnUrl;
        private readonly string _tmnCode;
        private readonly string _hashSecret;

        public VNpayController(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration["VNPay:Url"];
            _returnUrl = _configuration["VNPay:ReturnUrl"];
            _tmnCode = _configuration["VNPay:TmnCode"];
            _hashSecret = _configuration["VNPay:HashSecret"];
        }

        [HttpPost("payment")]
        public IActionResult Payment([FromBody] PaymentRequest model)
        {
            try
            {
                string clientIPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                VNPay pay = new VNPay();

                pay.AddRequestData("vnp_Version", "2.1.0");
                pay.AddRequestData("vnp_Command", "pay");
                pay.AddRequestData("vnp_TmnCode", _tmnCode);
                pay.AddRequestData("vnp_Amount", model.Amount);
                pay.AddRequestData("vnp_BankCode", model.BankCode ?? "");
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", "VND");
                pay.AddRequestData("vnp_IpAddr", clientIPAddress);
                pay.AddRequestData("vnp_Locale", "vn");
                pay.AddRequestData("vnp_OrderInfo", model.OrderInfo);
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", _returnUrl);
                pay.AddRequestData("vnp_TxnRef", model.OrderInfor);

                string paymentUrl = pay.CreateRequestUrl(_url, _hashSecret);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("paymentconfirm")]
        public IActionResult PaymentConfirm()
        {
            try
            {
                if (Request.QueryString.HasValue)
                {
                    var queryString = Request.QueryString.Value;
                    var json = HttpUtility.ParseQueryString(queryString);

                    long orderId = Convert.ToInt64(json["vnp_TxnRef"]);
                    string orderInfor = json["vnp_OrderInfo"].ToString();
                    long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
                    string vnp_ResponseCode = json["vnp_ResponseCode"].ToString();
                    string vnp_SecureHash = json["vnp_SecureHash"].ToString();
                    var pos = Request.QueryString.Value.IndexOf("&vnp_SecureHash");

                    bool checkSignature = ValidateSignature(Request.QueryString.Value.Substring(1, pos - 1), vnp_SecureHash, _hashSecret);
                    if (checkSignature && _tmnCode == json["vnp_TmnCode"].ToString())
                    {
                        if (vnp_ResponseCode == "00")
                        {
                            return Redirect("SUCCESS_LINK");
                        }
                        else
                        {
                            return Redirect("FAILURE_LINK");
                        }
                    }
                    else
                    {
                        return Redirect("INVALID_RESPONSE_LINK");
                    }
                }
                return Redirect("INVALID_RESPONSE_LINK");
            }
            catch (Exception ex)
            {
                // Log exception here
                return Redirect("ERROR_LINK");
            }
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = VNPay.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
