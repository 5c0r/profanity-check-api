namespace ProfanityCheck.WebAPI.Model
{
    public interface ICommonResponse<T> where T : class
    {
        bool Success { get; }
        T Data { get; set; }
    }
}
