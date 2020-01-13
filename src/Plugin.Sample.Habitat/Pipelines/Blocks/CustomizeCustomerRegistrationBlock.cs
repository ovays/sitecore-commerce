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
using Sitecore.Security.Authentication;

namespace Sitecore.Commerce.Plugin.Habitat.Pipelines.Blocks
{
    public class CustomizeCustomerRegistrationBlock : PipelineBlock<Customer, Customer, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly FindEntitiesInListCommand _findEntitiesInListCommand;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findEntitiesInListCommand"></param>
        public CustomizeCustomerRegistrationBlock(FindEntitiesInListCommand findEntitiesInListCommand)
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
            Condition.Requires<Customer>(customer).IsNotNull<Customer>("The customer can not be null");
            Condition.Requires<string>(customer.UserName).IsNotNullOrEmpty("The customer user name can not be null");
            // This is where we create a account in Salesforce by calling FuseIT service.
            CreateNewSalesforceAccount(context, _findEntitiesInListCommand,customer);         
            return Task.FromResult<Customer>(customer);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        /// <param name="findEntitiesInListCommand"></param>
        /// <returns></returns>
        private static void CreateNewSalesforceAccount(CommercePipelineExecutionContext context, FindEntitiesInListCommand findEntitiesInListCommand,Customer customerInfo)
        {
            //logic
            //Account account = new Account();
            //account.Name = "FuseIT";

            //SalesforceSession salesforceSession = new SalesforceSession(
            //    new LoginDetails("username@example.com", "salesforcePassword"));
            //AccountService accountService = new AccountService(salesforceSession);

            //var saveResult = accountService.Insert(account);
            //string newAccountId = null;
            //if (saveResult.success)
            //{
            //    newAccountId = account.Id;
            //}
            //Assert.IsTrue(accountService.ValidEntityId(newAccountId));
               var isLoggedIn =AuthenticationManager.Login(customerInfo.UserName, customerInfo.Password, false);
        }
    }
}
