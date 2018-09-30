using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class FieldInfo
    {
        #region Members
        public object Data { get; set; }

        private static Dictionary<Operation, Func<dynamic, dynamic, bool>> rules;
        private Operation[] operations = (Operation[])Enum.GetValues(typeof(Operation));
        private OperationInfo operationInfo;
        private string ruleText;
        private string typeName;
        private string fieldName;
        private string ownerClassName;
        #endregion
        #region Construction , Initialisation
        static FieldInfo()
        {
            InitDictionary();
        }
        public FieldInfo(string owner, string field, string type)
        {
            typeName = type;
            fieldName = field;
            ownerClassName = owner;
            Data = Activator.CreateInstance(Type.GetType(typeName), GetObject(typeName));
        }
        private static void InitDictionary()
        {
            rules = new Dictionary<Operation, Func<dynamic, dynamic, bool>>();
            rules.Add(Operation.Contains, (paramRule, data) => RuleContains(paramRule, data));
            rules.Add(Operation.GreaterThan, (paramRule, data) => RuleGreaterThan(paramRule, data));
            rules.Add(Operation.EqualTo, (paramRule, data) => RuleEquals(paramRule, data));
        }
        #endregion

        #region Rules
        private static bool RuleContains(dynamic data, dynamic value)
        {
            return data.ToUpper().Contains(value.ToUpper());
        }
        private static bool RuleGreaterThan(dynamic data, dynamic value)
        {
            return data > value;
        }
        private static bool RuleEquals(dynamic data, dynamic value)
        {
            return data.Equals(value);
        }
        #endregion
        #region Public Methods
        public void CreateOperationInfo(string value)
        {
            ruleText = value;
            operationInfo = new OperationInfo();
            operationInfo.OperationRule = operations.Where(x => ruleText.Contains(x.ToString())).FirstOrDefault();
            string splitParam = Regex.Split(ruleText, operationInfo.OperationRule.ToString())[1];
            operationInfo.ParamRule = DataConverter(splitParam.Trim());
        }
        public void Initialization()
        {
            Random getrandom = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            switch (typeName)
            {
                case "Null":
                    Data = null;
                    break;
                case "System.String":
                    int length = 10;
                    Data = new string(Enumerable.Repeat(chars, length)
                         .Select(s => s[getrandom.Next(s.Length)]).ToArray());
                    break;
                case "System.Boolean":
                    Data = Convert.ToBoolean(true);
                    break;
                case "System.DateTime":
                    int maxDate = 0;
                    int minDate = -9999;
                    int deltaDate = getrandom.Next(minDate, maxDate);
                    Data = DateTime.Today.AddDays(deltaDate);
                    break;
                case "System.Double":
                    double maximum = 78.87;
                    double minimum = 0.18;
                    Data = getrandom.NextDouble() * (maximum - minimum) + minimum; ;
                    break;
                case "System.Int32":
                    int max = 99999;
                    int min = 1;
                    Data = getrandom.Next(min, max);
                    break;
                default:
                    break;
            }
        }

        public object DataConverter(string value)
        {
            object result = null;
            switch (typeName)
            {
                case "System.Boolean":
                    result = Convert.ToBoolean(value);
                    break;
                case "System.DateTime":
                    result = Convert.ToDateTime(value);
                    break;
                case "System.Double":
                    result = Convert.ToDouble(value);
                    break;
                case "System.Int32":
                    result = Convert.ToInt32(value);
                    break;
                default:
                    result = value;
                    break;
            }
            return result;
        }

        public void Run(Action<string> action)
        {
            string ruleResult = string.Empty;
            try
            {
                Func<object, object, bool> ruleMethod;
                if (operationInfo == null)
                {
                    ruleResult = $"{DateTime.Now.ToString("HH:mm:ss")} Class {ownerClassName}  Fiels {fieldName} Value {Data} Rule ---";
                }
                else if (rules.TryGetValue(operationInfo.OperationRule, out ruleMethod))
                {
                    bool res = ruleMethod.Invoke(Data, operationInfo.ParamRule);
                    ruleResult = $"{DateTime.Now.ToString("HH:mm:ss")} Class {ownerClassName} Field {fieldName} Value {Data} Rule {ruleText} => Operation [{operationInfo.ParamRule}] Result:{res}";
                }
            }
            catch (Exception e)
            {
                ruleResult = $"Rule {ruleText} execution failure on {ownerClassName}.{fieldName}";
            }
            if(action != null)
            {
                action(ruleResult);
            }
        }
        #endregion

        #region Private Methods

        private object[] GetObject(string type)
        {
            if (type == "System.String")
            {
                return new object[] { new char[] { } };
            }
            return null;
        }

        #endregion
    }
}
