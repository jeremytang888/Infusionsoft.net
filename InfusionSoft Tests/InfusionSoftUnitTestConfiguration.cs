using InfusionSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfusionSoft_Tests
{
    public class InfusionSoftUnitTestConfiguration : IInfusionSoftConfiguration
    {

        public static string Application
        {
            get { return "tp161"; }
        }
        

        public static string APIKey
        {
            get { return "343357b381897f551a19165751fa84d7"; }
        }

        public string ApplicationName
        {
            get { return Application; }
        }

        public string GetApiKey()
        {
            return APIKey;
        }
    }
}
