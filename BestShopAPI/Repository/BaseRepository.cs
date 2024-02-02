namespace BestShopAPI.Repository
{
    public class BaseRepository : IBaseRepository
    {
        public string? connectionString;
        string IBaseRepository.connectionString => connectionString;

        public BaseRepository()
        {
            var currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
            connectionString = $"Server=(localdb)\\SQLEXPRESS;Integrated Security=true;Database=BestShopDB;AttachDbFileName={Path.Combine(currentDirectory, "BestShopDB.mdf")}";
        }

    }
}
