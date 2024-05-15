namespace starter_serv.DataProviders
{
    public class ConsumerStage
    {
        public int ProcessStatus;
        public Guid StageId;
        public string Code;

        public ConsumerStage(int processStatus, Guid stageId, string code)
        {
            ProcessStatus = processStatus;
            StageId = stageId;
            Code = code;
        }
    }
    public static class CommonConstants
    {

        public static Dictionary<string, Type> ConverterMapings = new Dictionary<string, Type>
        {
            { "string", typeof(string) },
            { "int", typeof(int) },
            { "bool",typeof(bool) },
            { "guid", typeof(Guid) },
            { "date", typeof(DateTime)},
            { "raw", typeof(string)},
        };


    }
}
