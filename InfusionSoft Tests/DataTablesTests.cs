using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfusionSoft;
using System.Collections.Generic;
using InfusionSoft.Tables;
using System.Linq;
using Moq;


namespace InfusionSoft_Tests
{
    [TestClass]
    public class DataTablesTests : InfusionSoftAPITest
    {
        [TestMethod]
        public void InfusionSoft_Test_Tables_Contact()
        {
            //Contact testUser = InfusionSoftClient.ContactService.FindByEmail("testemail@sharkindicators.com").First();
            Contact testUser = InfusionSoftClient.DataService.FindByField<Contact>(x => x.Add<int>(y => y.Id,7824)).FirstOrDefault();

            Assert.AreEqual(testUser.Email, "jeremytang@outlook.com");

        }
        [TestMethod()]
        public void InfusionSoft_Tests_Tables_Job()
        {
            // See what happens tomorrow when a second job/invoice gets added to the 2nd Recurring (does a 4th Job show up??)
            IEnumerable<Job> customerJobs = InfusionSoftClient.DataService.FindByField<Job>(x => x.Add<int>(y => y.ContactId, 4));

            Job job = customerJobs.First();

            Action<IQueryBuilder<OrderItem>> orderItemsQuery = (IQueryBuilder<OrderItem> qb) =>
            {
                qb.Add<int>(f => f.OrderId, job.Id);
            };

            


            // http://community.infusionsoft.com/showthread.php/1064-OrderItem-ItemType-value-of-12-for-Discounts
            // Copy: http://novaksolutions.com/orderitem-itemtype-values/
            IEnumerable<OrderItem> orderItems = InfusionSoftClient.DataService.Query<OrderItem>(orderItemsQuery).Where(x => x.ItemType == 4 || x.ItemType == 5 || x.ItemType == 9);

            OrderItem orderItem = orderItems.First();
            Assert.AreEqual(orderItem.ItemName, "Product2");
        }

        [TestMethod()]
        public void InfusionSoft_Tests_Tables_RecurringOrder()
        {
            IEnumerable<RecurringOrder> recurringOrders = InfusionSoftClient.DataService.FindByField<RecurringOrder>(x => x.Add<int>(y => y.ContactId, 4));

            foreach (RecurringOrder rOrder in recurringOrders)
            {
                int recurringId = rOrder.Id;
                IEnumerable<Job> jobsForRec = InfusionSoftClient.DataService.FindByField<Job>(x => x.Add<int>(y => y.JobRecurringId, recurringId));

                // Tommorrow: are there two such jobs now for the 2nd recurring order? and two such invoices?

                Job lastestJob = jobsForRec.OrderByDescending(x => x.DueDate).FirstOrDefault();

                if (lastestJob != null)
                {
                    IEnumerable<Invoice> invoices = InfusionSoftClient.DataService.FindByField<Invoice>(x => x.Add<int>(y => y.JobId, lastestJob.Id));
                    if (invoices.Aggregate(true, (acc, inv) => acc && inv.PayStatus == 1)) // all the invoices are paid
                    {
                        Product prod = InfusionSoftClient.DataService.FindByField<Product>(x => x.Add<int>(y => y.Id, rOrder.ProductId)).FirstOrDefault();

                        Assert.IsNotNull(prod);
                        if (rOrder.ProductId == 6)
                            Assert.AreEqual(prod.Sku, "PR3");
                        
                    }
                }
            }    
            
//            IEnumerable<JobRecurringInstance> instances = InfusionSoftClient.DataService.FindByField<JobRecurringInstance>(x=>x.Add<int>(y=>y.))

            //foreach (JobRecurringInstance jobInstance in instances)
            //{
            //    
            //}

        }
    }
}
