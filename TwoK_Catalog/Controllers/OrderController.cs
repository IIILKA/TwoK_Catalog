using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Dto.Order;
using TwoK_Catalog.Services.Interfaces;
using TwoK_Catalog.ViewModels.Order;

namespace TwoK_Catalog.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public OrderController(IUserService userService, ICartService cartService, IOrderService orderService)
        {
            _userService = userService;
            _cartService = cartService;
            _orderService = orderService;
        }

        public ViewResult ToOrder() => View(new CreateOrderViewModel());

        [HttpPost]
        public IActionResult ToOrder(CreateOrderViewModel createOrderViewModel)
        {
            var userId = _userService.GetUserId(User);

            if (_cartService.GetCartItems(userId).Count == 0)
            {
                ModelState.AddModelError("", "Прости, но твоя корзина пуста");
            }

            if (ModelState.IsValid)
            {
                var orderId = _orderService.CreateOrder(userId, createOrderViewModel);
                return RedirectToAction(nameof(Completed), new { orderId });
            }
            else
            {
                return View(createOrderViewModel);
            }
        }

        [AllowAnonymous]
        public IActionResult Completed(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(orderId);
            }
            else
            {
                return Redirect("/");
            }
        }

        //TODO: refactor this method
        public IActionResult SaveToPdf(int orderId)
        {
            var orderToPdf = _orderService.GetOrderPdf(orderId);

            if (orderToPdf != null)
            {
                var workStream = new MemoryStream();

                var pdfDocument = new Document(new Rectangle(400, 600), 30, 40, 30, 40);

                string strPDFFileName = string.Format($"Сheque №{orderId} " + DateTime.Now.ToString("yyyyMMdd") + "-" + ".pdf");

                PdfWriter.GetInstance(pdfDocument, workStream).CloseStream = false;
                pdfDocument.Open();

                var baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);
                var titleFont = new Font(baseFont, 20, 3);

                var p3 = new Paragraph
                {
                    SpacingAfter = 6
                };

                var title = new Paragraph($"Order №{orderToPdf.Id}", titleFont);
                title.Alignment = Element.ALIGN_CENTER;

                pdfDocument.Add(title);
                pdfDocument.Add(p3);

                var separator = new LineSeparator();
                pdfDocument.Add(separator);

                var orderData = new Paragraph($"Order Id: {orderToPdf.Id}\n" +
                                              $"Customer name: {orderToPdf.PersonName}\n" +
                                              $"Customer country: {orderToPdf.Country}\n" +
                                              $"Customer city: {orderToPdf.City}\n" +
                                              $"Customer address: {orderToPdf.Address}\n" +
                                              $"Customer post code: {orderToPdf.PostCode}\n", font);

                pdfDocument.Add(orderData);
                pdfDocument.Add(p3);

                var tableLayout = new PdfPTable(4);
                pdfDocument.Add(Add_Content_To_PDF(tableLayout, orderToPdf.OrderItems));

                pdfDocument.Close();
                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;
                return File(workStream, "application/pdf", strPDFFileName);
            }

            return RedirectToAction(nameof(Completed), orderId);
        }

        [Authorize(Roles = "SeniorAdmin,JuniorAdmin")]
        public IActionResult List()
        {
            return View(_orderService.GetOrders().Where(o => !o.IsShipped));
        }

        [Authorize(Roles = "SeniorAdmin,JuniorAdmin")]
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _orderService.GetOrders().FirstOrDefault(_ => _.Id == orderId);

            if(order != null)
            {
                _orderService.MarkOrderAsShipped(order.Id);
            }

            return RedirectToAction(nameof(List));
        }

        //TODO: move from controller
        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, List<OrderItemPdfDto> orderItems)
        {
            float[] headers = { 50, 35, 15, 35 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            var count = 1;
            //Add header  
            AddCellToHeader(tableLayout, "Product");
            AddCellToHeader(tableLayout, "Price");
            AddCellToHeader(tableLayout, "Quantity");
            AddCellToHeader(tableLayout, "Total");

            foreach (var orderItem in orderItems)
            {
                if (count >= 1)
                {
                    //Add body
                    AddCellToBody(tableLayout, orderItem.ProductTitle, count);
                    AddCellToBody(tableLayout, orderItem.ProductPrice.ToString("c"), count);
                    AddCellToBody(tableLayout, orderItem.Quantity.ToString(), count);
                    AddCellToBody(tableLayout, (orderItem.ProductPrice * orderItem.Quantity).ToString("c"), count);
                    count++;
                }
            }

            var totalPrice = orderItems.Sum(_ => _.ProductPrice * _.Quantity);
            tableLayout.AddCell(new PdfPCell(new Phrase(totalPrice.ToString("c"), new Font(Font.FontFamily.HELVETICA, 8, 3, BaseColor.BLACK)))
            {
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8,
                BackgroundColor = count % 2 == 0 ? new BaseColor(255, 255, 255) : new BaseColor(211, 211, 211)
            });

            return tableLayout;
        }

        //TODO: move from controller
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 8,
                BackgroundColor = new BaseColor(255, 255, 255)
            });
        }

        //TODO: move from controller
        private static void AddCellToBody(PdfPTable tableLayout, string cellText, int count)
        {
            if (count % 2 == 0)
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 8,
                    BackgroundColor = new BaseColor(255, 255, 255)
                });
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 8,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
            }
        }
    }
}
