using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using Sitecore.Commerce.Plugin.Customers;
namespace Sitecore.Commerce.Plugin.Sample1.Pipelines
{
    public class CreateCustomerMinionPipeline : CommercePipeline<Sitecore.Commerce.Plugin.Sample1.Models.SOMEOBJECTWITHCUSTOMERDATA, Customer>, ICreateCustomerMinionPipeline, IPipeline<Sitecore.Commerce.Plugin.Sample1.Models.SOMEOBJECTWITHCUSTOMERDATA, Customer, CommercePipelineExecutionContext>, IPipelineBlock<Sitecore.Commerce.Plugin.Sample1.Models.SOMEOBJECTWITHCUSTOMERDATA, Customer, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public CreateCustomerMinionPipeline(IPipelineConfiguration<ICreateCustomerMinionPipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {
        }
    }
}
