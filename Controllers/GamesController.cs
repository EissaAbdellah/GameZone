

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService _categoriesServices;
        private readonly IDevicesService _devicesServices;
        private readonly IGameService _gameServices;

        public GamesController(ApplicationDbContext context, ICategoriesService categoriesServices, IDevicesService devicesServices, IGameService gameServices)
        {
            _context = context;
            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
            _gameServices = gameServices;
        }


        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gameServices.getById(id);
            if (game is null)
                return NotFound();

            return View(game);
        }



        [HttpGet]
        public IActionResult Create()
        {


            CreateGameFromViewModel viewModel = new()
            {

                Categories = _categoriesServices.selectListItems(),
                Devices = _devicesServices.selectListItems(),

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.selectListItems();
                model.Devices = _devicesServices.selectListItems();
                return View(model);
            }

            //save to DB

            await _gameServices.Create(model);
            return RedirectToAction(nameof(Index));

        }




        public IActionResult Edit(int id)
        {
            var game = _gameServices.getById(id);
            if (game is null)
                return NotFound();


            EditGameFromViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Descreption = game.Descreption,
                CategoryId = game.CategoryId,
                Categories = _categoriesServices.selectListItems().ToList(),
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Devices = _devicesServices.selectListItems().ToList(),
                currentCover = game.Cover

            };

            return View(viewModel);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.selectListItems();
                model.Devices = _devicesServices.selectListItems();
                return View(model);
            }

            await _gameServices.Update(model);
            return RedirectToAction(nameof(Index));

        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var isDeleted = _gameServices.DeleteById(id);

            return (isDeleted) ? Ok() : BadRequest();


        }





    }
}
