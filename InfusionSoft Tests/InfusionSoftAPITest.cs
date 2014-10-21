using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfusionSoft_Tests
{
    public class InfusionSoftAPITest
    {
        protected InfusionSoft.IInfusionSoftClient _InfusionSoftClient;
        private InfusionSoft.Customer _InfusionSoftCustomerApp;
        protected InfusionSoft.IInfusionSoftClient InfusionSoftClient
        {
            get
            {
                if (_InfusionSoftClient == null)
                    Connect();
                return _InfusionSoftClient;
            }
        }
        protected void Connect()
        {
            _InfusionSoftCustomerApp = new InfusionSoft.Customer(InfusionSoftUnitTestConfiguration.Application, InfusionSoftUnitTestConfiguration.APIKey);
            _InfusionSoftClient = _InfusionSoftCustomerApp.Connect();
        }
    }
}
