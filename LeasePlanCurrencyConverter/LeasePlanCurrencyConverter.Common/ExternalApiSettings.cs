namespace LeasePlanCurrencyConverter.Common
{
    public class ExternalApiSettings
    {
        public string BaseAddress { get; set; }

        public string ApiKey { get; set; }

        public int RetryCount { get; set; }
    }
}
