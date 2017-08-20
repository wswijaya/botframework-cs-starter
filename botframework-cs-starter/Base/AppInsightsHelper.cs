namespace StarterBot.Base
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ApplicationInsights;
    using Microsoft.Bot.Connector;

    public class AppInsightsHelper
    {
        public static void TrackEvent(string eventName, IMessageActivity messageActivity = null, Dictionary<string, string> properties = null, Dictionary<string, double> metrics = null)
        {
            TelemetryClient telemetry = new TelemetryClient();
            if (messageActivity != null)
            {
                Dictionary<string, string> baseProperties = new Dictionary<string, string>
                {
                    {"ChannelId", messageActivity.ChannelId},
                    {"FromId", messageActivity.From.Id},
                    {"FromName", messageActivity.From.Name },
                    {"Message", messageActivity.Text},
                    {"ActivityType", messageActivity.Type}
                };
                if (properties != null)
                {
                    foreach (var item in baseProperties)
                    {
                        properties.Add(item.Key, item.Value);
                    }
                }
                else
                {
                    properties = baseProperties;
                }
            }
            telemetry.TrackEvent(eventName, properties, metrics);
        }

        public static void TrackException(Exception exception, Dictionary<string, string> properties = null, Dictionary<string, double> metrics = null)
        {
            TelemetryClient telemetry = new TelemetryClient();
            telemetry.TrackException(exception, properties, metrics);
        }

        public static void TrackTrace(string message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel severityLevel, Dictionary<string, string> properties = null)
        {
            TelemetryClient telemetry = new TelemetryClient();
            telemetry.TrackTrace(message, severityLevel, properties);
        }
    }
}