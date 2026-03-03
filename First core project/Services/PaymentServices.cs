using First_core_project.Services;
using System.Text;
using System.Text.Json;

public class PaymobPaymentService : IPaymentService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public PaymobPaymentService(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<string> CreatePaymentAsync(int orderId, decimal total)
    {
        var token = await GetAuthTokenAsync();
        var paymobOrderId = await RegisterOrderAsync(token, total, orderId);
        var paymentKey = await GetPaymentKeyAsync(token, paymobOrderId, total);

        var iframeId = _config["Paymob:IframeId"];

        return $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentKey}";
    }

    // ===========================
    // 1️⃣ Get Auth Token
    // ===========================
    private async Task<string> GetAuthTokenAsync()
    {
        var apiKey = _config["Paymob:ApiKey"];

        if (string.IsNullOrEmpty(apiKey))
            throw new Exception("Paymob ApiKey is missing from configuration.");

        var requestBody = new
        {
            api_key = apiKey
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            "https://accept.paymob.com/api/auth/tokens",
            content);

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Paymob Token Error: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("token").GetString();
    }

    // ===========================
    // 2️⃣ Register Order
    // ===========================
    private async Task<int> RegisterOrderAsync(string token, decimal amount, int merchantOrderId)
    {
        var requestBody = new
        {
            auth_token = token,
            delivery_needed = false,
            amount_cents = (long)(amount * 100),
            currency = "EGP",
            merchant_order_id = merchantOrderId.ToString(),
            items = Array.Empty<object>()
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            "https://accept.paymob.com/api/ecommerce/orders",
            content);

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Paymob Order Error: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("id").GetInt32();
    }

    // ===========================
    // 3️⃣ Get Payment Key
    // ===========================
    private async Task<string> GetPaymentKeyAsync(string token, int paymobOrderId, decimal amount)
    {
        var integrationId = int.Parse(_config["Paymob:IntegrationId"]);

        var requestBody = new
        {
            auth_token = token,
            amount_cents = (long)(amount * 100),
            expiration = 3600,
            order_id = paymobOrderId,
            billing_data = new
            {
                apartment = "NA",
                email = "test@user.com",
                floor = "NA",
                first_name = "Customer",
                street = "NA",
                building = "NA",
                phone_number = "01000000000",
                shipping_method = "NA",
                postal_code = "NA",
                city = "Cairo",
                country = "EG",
                last_name = "Name",
                state = "NA"
            },
            currency = "EGP",
            integration_id = integrationId
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            "https://accept.paymob.com/api/acceptance/payment_keys",
            content);

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Paymob PaymentKey Error: {json}");

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("token").GetString();
    }
}