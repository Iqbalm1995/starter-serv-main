namespace starter_serv.ViewModel
{
    public class RequestFilterParameter
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string ComparisonOperator { get; set; }
        public bool SwitchPosition { get; set; } = false;
    }
}
