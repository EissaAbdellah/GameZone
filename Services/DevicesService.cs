﻿namespace GameZone.Services
{
    public class DevicesService : IDevicesService
    {

        private readonly ApplicationDbContext _context;


        public DevicesService(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<SelectListItem> selectListItems()
        {
            var Devices = _context.Devices.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                 .OrderBy(c => c.Text)
                 .AsNoTracking()
                 .ToList();

            return Devices;
        }
    }
}
