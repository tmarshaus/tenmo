namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        decimal GetBalance(int userId);
    }
}