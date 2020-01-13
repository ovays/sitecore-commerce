using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Commerce.Plugin.Customers;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.CustomizeCustomerNumber.Pipelines.Blocks
{
    public class CustomizeCustomerNumberBlock : PipelineBlock<Customer, Customer, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly FindEntitiesInListCommand _findEntitiesInListCommand;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findEntitiesInListCommand"></param>
        public CustomizeCustomerNumberBlock(FindEntitiesInListCommand findEntitiesInListCommand)
        {
            _findEntitiesInListCommand = findEntitiesInListCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<Customer> Run(Customer customer, CommercePipelineExecutionContext context)
        {


            // Ensure customer is not null and has a name
            Condition.Requires<Customer>(customer).IsNotNull<Customer>( "The customer can not be null");
            Condition.Requires<string>(customer.UserName).IsNotNullOrEmpty( "The customer user name can not be null");


            // This is where we proseed to generating the new custom customer number either directly or by calling an external service.
            customer.AccountNumber = GenerateNewCustomerNumber(context, _findEntitiesInListCommand);
            customer.Id = $"{(object) CommerceEntity.IdPrefix<Customer>()}{(object) customer.AccountNumber}";
            customer.FriendlyId = customer.AccountNumber;

            return Task.FromResult<Customer>(customer);

        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        /// <param name="findEntitiesInListCommand"></param>
        /// <returns></returns>
        private static string GenerateNewCustomerNumber(CommercePipelineExecutionContext context, FindEntitiesInListCommand findEntitiesInListCommand)
        {
            // get all existing customers.
            var customers = (IEnumerable<Customer>)findEntitiesInListCommand.Process<Customer>(context.CommerceContext, CommerceEntity.ListName<Customer>(), 0, int.MaxValue).Result.Items;

            // Total existing customers
            var customerCount = customers.Count();

            if (!customers.Any()) return "customer1";

            // use the info you have to generate an appropriate customer number. You may also use the data you have to call an external system.
            // in this instance we will just return the number of existing customers incremented by 1
            // Return customer count and increment by 1 as the new customer number.

            var nextOrderNumber = customerCount + 1;
            return "customer" + nextOrderNumber.ToString();

        }


    }
}
