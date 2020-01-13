using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Customers;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Commerce.Plugin.Sample1.Pipelines
{
    [PipelineDisplayName("CreateCustomerMinionPipeline")]
    public interface ICreateCustomerMinionPipeline : IPipeline<Sitecore.Commerce.Plugin.Sample1.Models.SOMEOBJECTWITHCUSTOMERDATA, Customer, CommercePipelineExecutionContext>, IPipelineBlock<Sitecore.Commerce.Plugin.Sample1.Models.SOMEOBJECTWITHCUSTOMERDATA,  Customer, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}
