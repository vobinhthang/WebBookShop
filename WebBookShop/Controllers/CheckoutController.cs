using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Commons;
using WebBookShop.Commons;
using WebBookShop.Models;
using WebBookShop.PaymentService;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            var cartSession = Session["CartSession"];
            if (cartSession != null)
            {
                if(Session["LOGIN_CLIENT"] == null)
                {
                    return View();
                }
                var service = new UserService();
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                
                return View(account);
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [ChildActionOnly]
        public ActionResult ItemCheckOut()
        {
            var cartSession = Session["CartSession"];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;
            }

            return PartialView(list);
        }

        
        public ActionResult Payment()
        {
            var model = SharedData.customerAddress;
            var cartSession = Session["CartSession"];
            if (cartSession != null)
            {
                if (Session["LOGIN_CLIENT"] == null)
                {                   
                    return View(model);
                }

                var service = new UserService();
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                var customerAddress = new CustomerAddress
                {
                    _name = account.Fullname,
                    _phone = account.Phone,
                    _email = account.Email,
                    _address = account.Address,
                };

                SharedData.UserId = account.Id;
                SharedData.customerAddress = customerAddress;
                return View(customerAddress);
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [HttpPost]
        public ActionResult Index(int chose, UserModel user)
        {         
            if (chose == 1)
            {               
                return Redirect("/checkout/address");
            }
            else if(chose==2)
            {
                var service = new UserService();
                var result = service.Login(user.Email, Encryptor.GetMd5Hash(user.Password), "Khách hàng");

                if (user.Email != null && user.Password != null)
                {
                    if (result)
                    {
                        var name = service.FindName(user.Email);
                        Session["LOGIN_CLIENT"] = user.Email;
                        Session["NameAccount"] = name;
                        SharedData.Email = null;
                        SharedData.Password = null;
                        
                    }
                    else
                    {
                        
                        TempData["MESSAGE"] = "Email hoặc mật khẩu không đúng!";
                    }
                }
                return Redirect("/checkout");
            }
            return View();
        }

        public ActionResult Address()
        {
            
            var cartSession = Session["CartSession"];
            if (cartSession != null)
            {
                if (SharedData.customerAddress != null)
                {
                    var model = SharedData.customerAddress;
                    return View(model);
                }
                return View();
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [HttpPost]
        public ActionResult Address(string name, string phone, string email, string address)
        {
            var customerAddress = new CustomerAddress
            {
                _name = name,
                _phone=phone,
                _email=email,
                _address = address,
            };
            
            SharedData.customerAddress = customerAddress;
           
            return Redirect("/checkout/payment");
        }

        public ActionResult Confirmation()
        {
            var model = SharedData.customerAddress;

            var cartSession = Session["CartSession"];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;
                var count = list.Sum(x => x.Quantity);
                ViewBag.CountItem = count;
                ViewBag.ListItems = list;
                ViewBag.OrderId = SharedData.OrderId;
               
                if (Session["LOGIN_CLIENT"] == null)
                {
                    Session["CartSession"] = null;
                    return View(model);
                }

                var service = new UserService();
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                var customerAddress = new CustomerAddress
                {
                    _name = account.Fullname,
                    _phone = account.Phone,
                    _email = account.Email,
                    _address = account.Address,
                };
                
                SharedData.UserId = account.Id;
                Session["CartSession"] = null;
                return View(customerAddress);
            }
            else
            {
                return Redirect("/cart");
            }

        }

        [HttpPost]
        public ActionResult Payment(int chose)
        {
            if(chose == 1)
            {
                var service = new OrderService();

                var cartSession = Session["CartSession"];
                var list = new List<CartItem>();
                if (cartSession != null)
                {
                    list = (List<CartItem>)cartSession;

                }
                if (Session["LOGIN_CLIENT"] == null)
                {
                    var orderId = service.ConfirmPayment(list, SharedData.customerAddress, null, false,1);
                    SharedData.OrderId = orderId;
                }
                else
                {
                    var orderId = service.ConfirmPayment(list, SharedData.customerAddress, SharedData.UserId, true,1);
                    SharedData.OrderId = orderId;
                }
                
                return Redirect("/checkout/confirmation");
            }
            else 
            {
                return Redirect("/checkout/paymentonline");
            }
        }

        public ActionResult PaymentOnline()
        {
            var service = new OrderService();
            var cartSession = Session["CartSession"];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;

            }
            int orderId;
            if (Session["LOGIN_CLIENT"] == null)
            {
                orderId = service.ConfirmPayment(list, SharedData.customerAddress, SharedData.UserId, false, 2);
            }
            else
            {
                orderId = service.ConfirmPayment(list, SharedData.customerAddress, SharedData.UserId, true, 2);

            }

            SharedData.OrderId = orderId;
            var order = service.GetOrderById(orderId);
            var totalPrice = service.UpdateTotalPrice(orderId);
            //vnpay
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (Convert.ToString((totalPrice * 100)))); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Ultil.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng: " + orderId); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", orderId.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }
        public ActionResult PaymentOnlineConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về
                double totalPrice = Convert.ToDouble(pay.GetResponseData("vnp_Amount"))/100; //mã hóa đơn

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Success = "success";
                        ViewBag.TranId =  Convert.ToString(vnpayTranId);
                        ViewBag.TotalPrice = totalPrice;
                        Session["CartSession"] = null;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn  Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }
    }
}