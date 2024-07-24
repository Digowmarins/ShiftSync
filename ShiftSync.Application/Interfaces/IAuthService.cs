namespace ShiftSync.Application.Interfaces
{
    public interface IAuthService
    {
        string ComputeSha256Hash(string password);
    }
}