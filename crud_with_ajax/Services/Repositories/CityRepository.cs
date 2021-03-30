using crud_with_ajax.Models;
using crud_with_ajax.Persistence;
using crud_with_ajax.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace crud_with_ajax.Services.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CityExist(int Id)
        {
            return _context.Cities.Any(r => r.Id == Id);
        }

        public bool Create(City city)
        {
            _context.Add(city);
            return Save();
        }

        public bool Delete(City city)
        {
            _context.Remove(city);
            return Save();
        }

        public ICollection<City> GetCities()
        {
            return _context.Cities.OrderBy(r => r.Name).ToList();
        }

        public City GetCity(int Id)
        {
            City c = new City();
            c = _context.Cities.FirstOrDefault(r => r.Id == Id);
            return c;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool Update(City city)
        {
            _context.Update(city);
            return Save();
        }
    }
}
