using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace BL
{
    public enum Operation
    {
        EqualTo,
        Contains,
        StartsWith,
        EndsWith,
        NotEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo
    }

    public class OperationInfo
    {
        #region Members
        public dynamic ParamRule { get; set; }
        public Operation OperationRule { get; set; }
        public string RuleText { get; set; }
     
        #endregion
     
    }
}
