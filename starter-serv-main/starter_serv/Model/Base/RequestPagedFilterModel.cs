namespace starter_serv.Model
{
    public class RequestPagedFilterModel
    {
        public string? search { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public List<RequestWhereQuery> FilterWhere { get; set; }
        public List<string> FieldOrder { get; set; }
        public string? OrderDir { get; set; }
    }

    public class RequestWhereQuery
    {
        public string Field { get; set;}
        public string Value { get; set; }
        public string Operator { get; set; }
    }

    public class ProjectRequestPagedFilterModel
    {
        public string? search { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public List<RequestWhereQuery> FilterWhere { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public List<string> FieldOrder { get; set; }
        public string? OrderDir { get; set; }
    }


    public class DashboardDepartementRequestPagedFilterModel
    {
        public string? search { get; set; }
        //public int limit { get; set; }
        //public int page { get; set; }
        public List<RequestWhereQuery> FilterWhere { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        //public List<string> FieldOrder { get; set; }
        //public string? OrderDir { get; set; }
    }

    public class RequestReportPagedFilterModel<T>
    {
        public string? search { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public T FilterWhere { get; set; }
        public List<string> FieldOrder { get; set; }
        public string? OrderDir { get; set; }
    }
}
