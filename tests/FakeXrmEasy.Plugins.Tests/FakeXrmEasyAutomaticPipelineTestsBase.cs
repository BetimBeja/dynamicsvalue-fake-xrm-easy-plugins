using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.Enums;
using FakeXrmEasy.Middleware;
using FakeXrmEasy.Middleware.Crud;
using FakeXrmEasy.Middleware.Messages;
using FakeXrmEasy.Middleware.Pipeline;
using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Reflection;

namespace FakeXrmEasy.Plugins.Tests
{
    public class FakeXrmEasyAutomaticPipelineTestsBase
    {
        protected readonly IXrmFakedContext _context;
        protected readonly IOrganizationService _service;

        public FakeXrmEasyAutomaticPipelineTestsBase()
        {
            _context = MiddlewareBuilder
                        .New()
       
                        // Add* -> Middleware configuration
                        .AddCrud()   
                        .AddFakeMessageExecutors()
                        .AddPipelineSimulation(new PipelineOptions()
                        {
                            UseAutomaticPluginStepRegistration = true,
                            PluginAssemblies = new List<Assembly>()
                            {
                                Assembly.GetAssembly(typeof(FollowupPlugin))
                            }
                        })

                        // Use* -> Defines pipeline sequence
                        .UsePipelineSimulation()
                        .UseCrud() 
                        .UseMessages()

                        .SetLicense(FakeXrmEasyLicense.RPL_1_5)
                        .Build();
                        
            _service = _context.GetOrganizationService();
        }
    }
}
