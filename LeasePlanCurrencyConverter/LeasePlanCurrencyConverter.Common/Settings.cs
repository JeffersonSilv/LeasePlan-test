using System;
using System.IO;
using System.Reflection;

namespace LeasePlanCurrencyConverter.Common
{
    public class Settings
    {
        public String CurrenciesFileName { get; set; }

        public string GetCurrenciesFullPath()
        {
            var uri = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), CurrenciesFileName));

            return uri.AbsolutePath;
        }
    }
}
