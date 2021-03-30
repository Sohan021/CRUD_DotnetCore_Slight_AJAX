using crud_with_ajax.Models;
using System.Collections.Generic;

namespace crud_with_ajax.Services.Interfaces
{
    public interface IProfileRepository
    {
        ICollection<City> GetCities();
        City GetCity(int Id);
        bool CityExist(int Id);

        bool Create(City city);
        bool Update(City city);
        bool Delete(City city);
        bool Save();
    }
}
