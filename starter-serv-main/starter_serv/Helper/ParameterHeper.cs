namespace starter_serv.Helper
{
    public static class ParameterHeper
    {
        #region "Private Access"
        public static string GetComparisonOperator(string comparisonOperator)
        {
            var result = "";
            switch (comparisonOperator)
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
                case "<=":
                    result = " <= ";
                    break;
                case ">=":
                    result = " >= ";
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

        public static string GetClosedTagComparisonOperator(string comparisonOperator)
        {
            var result = "";
            switch (comparisonOperator)
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
        public static string GetConverter(object value)
        {
            var result = "";
            switch (value)
            {
                case Guid:
                    result = ".ToString()";
                    break;
            }
            return result;
        }
        public static object GetValue(object value)
        {
            var result = value;
            switch (value)
            {
                case Enum:
                    result = (int)value;
                    break;
            }
            return result;
        }

        //protected IQueryable<T> _Includes(IQueryable<T> query, string[] includes = null)
        //{
        //    if (includes != null && includes.Length > 0)
        //    {
        //        foreach (var include in includes)
        //        {
        //            query = query.Include(include);
        //        }
        //    }
        //    return query;
        //}
        public static bool IsUseDoubleQuote(object value)
        {
            switch (value)
            {
                case int:
                case Enum:
                    return false;
            }
            return true;
        }

        #endregion
    }

}
