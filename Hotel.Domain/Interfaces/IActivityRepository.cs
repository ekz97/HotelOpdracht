﻿using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IActivityRepository
    {
        IReadOnlyList<Activity> GetActivitiesByOrganiserId(int id);
        IReadOnlyList<Description> GetAllDescriptions();
        IReadOnlyList<PriceInfo> GetAllPriceInfos();

        public void AddActivity(Activity activity, int organiserId);
        public void UpdateActivity(Activity activity);
        public void DeleteActivity(int activityid);

    }
}
