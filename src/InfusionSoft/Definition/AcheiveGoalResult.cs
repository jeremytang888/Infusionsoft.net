using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfusionSoft.Definition
{
    public class AcheiveGoalResult
    {
        public bool success { get; set; }
        public int funnelId { get; set; }
        public int goalId { get; set; }
        public Flow[] flowStart { get; set; }
        public Flow[] flowStop { get; set; }

    }

    public class Flow
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public int flowId { get; set; }

    }
}
