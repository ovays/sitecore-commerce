using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using Sitecore.Commerce.Plugin.Sample1.Models;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Plugin.Customers;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.EntityViews;

namespace Sitecore.Commerce.Plugin.Sample1.Pipelines.Blocks
{
    [PipelineDisplayName("CreateCustomerMinionPipeline.CreateCustomerBlock")]

    public class CreateCustomersBlock : PipelineBlock<SOMEOBJECTWITHCUSTOMERDATA, Customer, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;
        private readonly FindEntityPipeline _findEntityPipeline;
        private readonly CreateCustomerPipeline _createCustomerPipeline;
        private readonly PersistEntityPipeline _persistEntityPipeline;
        public CreateCustomersBlock(CommerceCommander commerceCommander, FindEntityPipeline findEntityPipeline, CreateCustomerPipeline createCustomerPipeline, PersistEntityPipeline persistEntityPipeline)
        {
            _commerceCommander = commerceCommander;
            _findEntityPipeline = findEntityPipeline;
            _createCustomerPipeline = createCustomerPipeline;
            _persistEntityPipeline = persistEntityPipeline;
        }
        public override async Task<Customer> Run(SOMEOBJECTWITHCUSTOMERDATA arg, CommercePipelineExecutionContext context)
        {
            var propertiesPolicy = context.GetPolicy<CustomerPropertiesPolicy>();
            var customer = new Customer();
            string friendlyId = Guid.NewGuid().ToString("N");

            customer.AccountNumber = friendlyId;
            customer.FriendlyId = friendlyId;
            customer.Id = $"{CommerceEntity.IdPrefix<Customer>()}{friendlyId}";

            customer.Email = arg.EmailAddress;
            
           


            //add customer to the entity index so that we can search later
            await this._persistEntityPipeline.Run(
            new PersistEntityArgument(
                new EntityIndex
                {
                    Id = $"{EntityIndex.IndexPrefix<Customer>("Id")}{customer.Id}",
                    IndexKey = customer.Id,
                    EntityId = customer.Id
                }),
            context);

            return customer;
        }
    }
}
