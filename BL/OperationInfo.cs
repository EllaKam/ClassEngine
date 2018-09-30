using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion
        #region Construction
        public OperationInfo()
        {

        }
        public OperationInfo(OperationInfo operation)
        {
            ParamRule = operation.ParamRule;
            OperationRule = operation.OperationRule;
        }
        #endregion

    }
}
