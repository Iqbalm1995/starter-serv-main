namespace starter_serv.Model
{
    public class QueryPagedFilterModel
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

    public class QueryPagedDepartementProjectFilterModel
    {
        public string? search { get; set; }
        //public int limit { get; set; }
        //public int page { get; set; }
        public List<RequestWhereQuery>? FilterWhere { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? yearNumber { get; set; }
        public int? DepartementId { get; set; }
        public int? UserId { get; set; }
        public List<string> FilterKategori { get; set; }
        //public List<string> FieldOrder { get; set; }
        //public string? OrderDir { get; set; }
    }
    public class QueryReportPagedFilterModel<T>
    {
        public string? search { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public T FilterWhere { get; set; }
        public int? yearNumber { get; set; }
        public int? DepartementId { get; set; }
        public int? UserId { get; set; }
        public List<string> FieldOrder { get; set; }
        public string? OrderDir { get; set; }
    }
}
