namespace StarterBot.Base
{
    using System.Configuration;
    public class ConfigurationHelper
    {
        public static string ReadSetting(string key)
        {
            string result = string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? string.Empty;
            }
            catch (ConfigurationErrorsException ex)
            {
                System.Diagnostics.Trace.TraceError($"{ex.GetType().FullName} {ex.Message}");
            }
            return result;
        }
    }
}