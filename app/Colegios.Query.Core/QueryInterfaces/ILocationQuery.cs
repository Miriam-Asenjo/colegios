﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.Core.QueryInterfaces
{
    public interface ILocationQuery
    {
        IEnumerable<CityDto> GetCities();

        IEnumerable<LocalityDto> GetLocations();

        IEnumerable<TownDto> GetTowns();

        TownDto GetTown(int townId);

        LocalityDto GetLocation(int localityId);
    }
}
