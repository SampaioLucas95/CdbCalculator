namespace CDBCalculator.Application.Shared
{
    public class GenericCommandResult<T>
    {
        public GenericCommandResult(bool success, T data, List<string>? errors = null)
        {
            Success = success;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string>? Errors { get; set; }

        public void AddError(string error)
        {
            Errors ??= new List<string>();
            Errors.Add(error);
        }
    }    
}
