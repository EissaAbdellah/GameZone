namespace GameZone.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();

        Game getById(int id);

        Task Create(CreateGameFromViewModel model);

        Task Update(EditGameFromViewModel model);

        bool DeleteById(int id);
    }
}
