using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Colegios.Query.Core.QueryInterfaces;
using Colegios.Query.Core.DTOs;
using Colegios.Query.Core.Criteria;
using SharpArch.NHibernate;
using Colegios.Query.Core.Entities.Lookups;

namespace Colegios.Query
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class NurseryQuery: NHibernateQuery, INurseryQuery 
    {
        public IEnumerable<NurseryDto> FindNurseries(NurseryCriteria criteria, int pageSize, out int totalCount, out IList<HighLightNurseryDto> highLightedNurseries, int highLightedNum, int pageNo = 1)
        {
            highLightedNurseries = new List<HighLightNurseryDto>();
            var pastHighLightedIds = 0;
            if (criteria.Localities.Any())
            {
                totalCount = 0;

                var query = Session.QueryOver<NurseryDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);

                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.IsBilingue == criteria.IsBilingue);

                if (criteria.Localities.Count() == 1)
                {
                    HighLightNurseryDto highLightNursery = null;
                    var hightLightedQuery = Session.QueryOver<HighLightNurseryDto>(()=> highLightNursery).Where(x => x.StartDate <= DateTime.Now).And(x => x.EndDate >= DateTime.Now)
                                            .JoinQueryOver(x=> x.Nursery).Where(x=> x.Locality.Id == criteria.Localities.First());

                    if (criteria.EducationType != null)
                        hightLightedQuery = hightLightedQuery.Where(x => x.Type.Id == criteria.EducationType);

                    if (!string.IsNullOrEmpty(criteria.Keywords))
                        hightLightedQuery = hightLightedQuery.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                    if (criteria.IsBilingue)
                        hightLightedQuery = hightLightedQuery.Where(x => x.IsBilingue == criteria.IsBilingue);

                    highLightedNurseries = hightLightedQuery.OrderBy(() => highLightNursery.StartDate).Asc.List();
                }

                var highLightedIds = highLightedNurseries.Select(x => x.Nursery.Id).ToList();

                totalCount = query
                            .AndRestrictionOn(x => x.Locality.Id).IsIn(criteria.Localities)
                            .Select(Projections.RowCount()).SingleOrDefault<int>();

                query = Session.QueryOver<NurseryDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);
                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.IsBilingue == criteria.IsBilingue);

                if (highLightedNurseries.Any())
                {
                    var hqlHighLighted = "select nursery.Id from Nurseries nursery "
                                        + " inner join lookup_localities locality on nursery.localityId = locality.Id ";
                                          
                    var conditions = new List<string>();
                    conditions.Add(" locality.Id = " + criteria.Localities.First());
                    if (criteria.EducationType != null)
                        conditions.Add(" nursery.type=" + criteria.EducationType);
                    if (!string.IsNullOrEmpty(criteria.Keywords))
                        conditions.Add(" nursery.name like '%" + criteria.Keywords + "%'");
                    if (criteria.IsBilingue)
                        conditions.Add(" nursery.Bilingue=" + criteria.IsBilingue);

                    hqlHighLighted += " where ";

                    for (var index = 0; index < conditions.Count(); index++)
                    {
                        if (index == 0)
                            hqlHighLighted += conditions[index];
                        else hqlHighLighted += " and " + conditions[index];
                    } 

                    hqlHighLighted += " order by nursery.Name asc limit 0," + ((pageNo - 1) * pageSize);


                    pastHighLightedIds = Session.CreateSQLQuery(hqlHighLighted).List<int>().Intersect<int>(highLightedIds).Count();
                }

                var nurseries = query
                        .AndRestrictionOn(x => x.Locality.Id).IsIn(criteria.Localities)
                        .OrderBy(x => x.Name).Asc()
                        .Skip(((pageNo - 1) * pageSize) + pastHighLightedIds)
                        .Take(pageSize + highLightedNum)
                        .List<NurseryDto>();


                return nurseries.Except(highLightedNurseries.Select(x=> x.Nursery)).Take(pageSize);

            }
            else //looking by townid
            {
                totalCount = 0;

                var query = Session.QueryOver<NurseryDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);

                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.IsBilingue == criteria.IsBilingue);

                totalCount = query
                            .JoinQueryOver(x => x.Locality).Where(x => x.Town.Id == criteria.TownId)
                            .Select(Projections.RowCount()).SingleOrDefault<int>();


                query = Session.QueryOver<NurseryDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);
                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.IsBilingue == criteria.IsBilingue);

                return query
                              .JoinQueryOver(x => x.Locality).Where(x => x.Town.Id == criteria.TownId)
                              .OrderBy(x => x.Name).Asc()
                              .Skip((pageNo - 1) * pageSize).Take(pageSize)
                              .List();
            }
        }


        public NurseryDto GetNursery(int nurseryId)
        {
            //Load and Get are Hibernate methods to retrieve an entity based on its Id
            //Load retrives a proxy to object or an exception, it should be used when a object certainly exist
            //Get retrieves null otherwise retrives the instance
            return Session.Load<NurseryDto>(nurseryId);
        }

        public IEnumerable<NurseryDto> DistrictNurseries(int localityId)
        {
            return
                Session.QueryOver<NurseryDto>().JoinQueryOver(x => x.Locality).Where(x => x.Id == localityId)
                    .List();
        }

        public IList<CategoryFieldInstitutionTypeDto> GetCategoryFields()
        {
            return Session.QueryOver<CategoryFieldInstitutionTypeDto>().Where(x=> x.InstitutionType == InstitutionType.Guarderia).List();
        }
    }
}
