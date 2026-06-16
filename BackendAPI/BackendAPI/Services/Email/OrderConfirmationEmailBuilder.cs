using System.Globalization;
using System.Net;

namespace BackendAPI.Services.Email;

public static class OrderConfirmationEmailBuilder
{
    public static string BuildSubject(OrderConfirmationEmail email)
    {
        return $"Laptop Store - Order #{email.InvoiceId} confirmed";
    }

    public static string BuildBody(OrderConfirmationEmail email)
    {
        var culture = CultureInfo.GetCultureInfo("vi-VN");
        var rows = string.Join(
            string.Empty,
            email.Items.Select(item =>
                $"<tr><td>{WebUtility.HtmlEncode(item.ProductName)}</td><td>{item.Quantity}</td><td>{item.Price.ToString("#,##0 VND", culture)}</td></tr>"));

        return $"""
            <h2>Dat hang thanh cong</h2>
            <p>Cam on ban da dat hang tai Laptop Store. Don hang #{email.InvoiceId} da duoc ghi nhan.</p>
            <p><strong>Nguoi nhan:</strong> {WebUtility.HtmlEncode(email.Receiver)}</p>
            <p><strong>Dia chi:</strong> {WebUtility.HtmlEncode(email.Address)}</p>
            <p><strong>So dien thoai:</strong> {WebUtility.HtmlEncode(email.Phone)}</p>
            <table border="1" cellpadding="8" cellspacing="0">
                <thead><tr><th>San pham</th><th>So luong</th><th>Don gia</th></tr></thead>
                <tbody>{rows}</tbody>
            </table>
            <p><strong>Tong tien:</strong> {email.Total.ToString("#,##0 VND", culture)}</p>
            """;
    }
}
