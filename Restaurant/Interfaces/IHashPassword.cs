namespace Restaurant.Interfaces
{
    public interface IHashPassword
    {
        string ComputePasswordHashing(string rowPassword);
    }
}
