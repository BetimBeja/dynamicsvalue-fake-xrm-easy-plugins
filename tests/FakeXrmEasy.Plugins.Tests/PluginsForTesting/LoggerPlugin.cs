#if FAKE_XRM_EASY_9
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class LoggerPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.Get<ILogger>();
            logger.LogInformation("Test");
        }
    }
}
#endif