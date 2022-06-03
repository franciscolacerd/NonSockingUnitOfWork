namespace NSUOW.Persistence.Repositories
{
    public class BaseRepository
    {
        public int Skip(int page, int pageSize) { return (page - 1) * pageSize; }
    }
}
