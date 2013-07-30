using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using Colegios.Query.Core.QueryInterfaces;
using SharpArch;
using SharpArch.NHibernate;

namespace Colegios.Query
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class LocationQuery: NHibernateQuery, ILocationQuery
    {
        public IEnumerable<LocalityDto> GetLocations()
        {
            IEnumerable<LocalityDto> localities = Session.QueryOver<LocalityDto>().Cacheable().List();
            return localities;
        }

        public LocalityDto GetLocation(int localityId)
        {
            return Session.QueryOver<LocalityDto>().Where(x=> x.Id == localityId).SingleOrDefault();
        }

        public IEnumerable<TownDto> GetTowns()
        {
            IEnumerable<TownDto> towns = Session.QueryOver<TownDto>().Cacheable().List();
            return towns;
        }

        public TownDto GetTown(int townId)
        {
            return Session.QueryOver<TownDto>().Where(x => x.Id == townId).SingleOrDefault();
        }

        public IEnumerable<CityDto> GetCities()
        {
            IEnumerable<CityDto> cities = Session.QueryOver<CityDto>().Cacheable().List();
            return cities;
        }
    }
}
