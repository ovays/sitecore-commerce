// © 2016 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Plugin.Sample.Habitat
{
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Customers;
    using Sitecore.Commerce.Plugin.Habitat.Pipelines;
    using Sitecore.Commerce.Plugin.Habitat.Pipelines.Blocks;
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

    /// <summary>
    /// The Habitat configure class.
    /// </summary>
    /// <seealso cref="IConfigureSitecore" />
    public class ConfigureSitecore : IConfigureSitecore
    {
      
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config

                .ConfigurePipeline<ICreateCustomerPipeline>(c => { c.Add<CustomizeCustomerRegistrationBlock>().After<CreateCustomerBlock>(); })
            );

            services.RegisterAllCommands(assembly);
        }
    }
}
