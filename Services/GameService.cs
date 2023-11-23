namespace GameZone.Services
{
    public class GameService : IGameService
    {

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _imagPath;

        public GameService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";


        }


        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(c => c.Category)
                .Include(d => d.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }


        public Game getById(int id)
        {


            return _context.Games
                .Include(c => c.Category)
                .Include(d => d.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }




        public async Task Create(CreateGameFromViewModel model)
        {


            /*   var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
               var path = Path.Combine(_imagPath, coverName);


               using var stream = File.Create(path);

               await model.Cover.CopyToAsync(stream);
               // stream.Dispose(); */
            var coverName = await SaveCover(model.Cover);


            Game game = new Game()
            {

                Name = model.Name,
                CategoryId = model.CategoryId,
                Descreption = model.Descreption,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };


            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task Update(EditGameFromViewModel model)
        {

            var game = _context.Games
            .Include(g => g.Devices)
            .SingleOrDefault(g => g.Id == model.Id);


            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;


            game.Name = model.Name;
            game.Descreption = model.Descreption;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();


            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover);
                await _context.SaveChangesAsync();


            }


            var cover = Path.Combine(_imagPath, oldCover);
            File.Delete(cover);
            await _context.SaveChangesAsync();




        }


        public bool DeleteById(int id)
        {
            var isDeletd = false;
            var game = _context.Games.Find(id);
            if (game is null)
                return false;
            _context.Games.Remove(game);
            if (_context.SaveChanges() > 0)
            {

                isDeletd = true;
                var cover = Path.Combine(_imagPath, game.Cover);
                File.Delete(cover);
            }


            return isDeletd;
        }




        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagPath, coverName);


            using var stream = File.Create(path);

            await cover.CopyToAsync(stream);

            return coverName;
        }


    }
}
