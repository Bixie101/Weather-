using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration
{
  public interface ITourismDestinationService
    {
        IEnumerable<TourismDestination> GetAllDestinations();
        TourismDestination GetDestinationById(int id);
        void AddDestination(TourismDestination destination);
        void UpdateDestination(TourismDestination destination);
        void DeleteDestination(int id);
    }
}
