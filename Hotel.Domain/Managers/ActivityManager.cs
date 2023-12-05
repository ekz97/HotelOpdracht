using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public IReadOnlyList<Activity> GetActivitiesByOrganiserId(int organiserId)
        {
            return _activityRepository.GetActivitiesByOrganiserId(organiserId);
        }
    }
}
