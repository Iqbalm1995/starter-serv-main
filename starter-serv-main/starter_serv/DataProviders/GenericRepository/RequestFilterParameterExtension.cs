using starter_serv.ViewModel;
using System.Reflection;

namespace starter_serv.DataProviders
{
    public static class RequestFilterParameterExtension
    {
        public static object GetFilterValue(this RequestFilterParameter filter)
        {
            if (CommonConstants.ConverterMapings[filter.Type].IsEnum)
            {
                return Enum.Parse(CommonConstants.ConverterMapings[filter.Type], filter.Value);
            }
            else if (CommonConstants.ConverterMapings[filter.Type] == typeof(Guid))
            {
                Guid guid = Guid.TryParse(filter.Value, out guid) ? guid : Guid.Empty;
                return guid;
            }
            else
            {
                return Convert.ChangeType(filter.Value, CommonConstants.ConverterMapings[filter.Type]);
            }
        }
        public static Type GetFilterType(this RequestFilterParameter filter)
        {
            Type type = CommonConstants.ConverterMapings.TryGetValue(filter.Type, out type) ? type : typeof(string);
            return type;
        }
        public static System.Linq.Expressions.LambdaExpression GetFilterEnumLambda(this RequestFilterParameter filter)
        {
            Type type = typeof(RequestFilterParameterExtension);
            MethodInfo method = type.GetMethod(string.Format("GetFilterEnumLambda{0}", filter.Type));
            System.Linq.Expressions.Expression<Func<object, bool>> defaultValue = x => true;
            var result = method != null ? (System.Linq.Expressions.LambdaExpression)method.Invoke(null, new object[] { filter.Value }) : defaultValue;
            return result;
        }

        private static string _GetStringEnumValue(object value)
        {
            return value.ToString();
        }

        public static string GetComparasionType(this RequestFilterParameter value)
        {
            var result = "";
            switch (value.ComparisonOperator)
            {
                case "like":
                    result = ".Contains(";
                    break;
                case "not like":
                    result = ".Contains(";
                    break;
                case "!=":
                    result = "!=";
                    break;
                case "<":
                    result = " < ";
                    break;
                case ">":
                    result = " > ";
                    break;
                case "<>":
                    result = " <> ";
                    break;
                default:
                    result = "=";
                    break;
            }
            return result;
        }

        public static string GetClosedTagComparisonOperator(this RequestFilterParameter value)
        {
            var result = "";
            switch (value.ComparisonOperator)
            {
                case "like":
                    result = ")";
                    break;
                case "not like":
                    result = ") == false";
                    break;
            }
            return result;
        }

        public static bool IsUseDoubleQuote(this RequestFilterParameter value)
        {
            switch (value.GetFilterValue())
            {
                case int:
                case Enum:
                    return false;
            }
            return true;
        }
    }
}
