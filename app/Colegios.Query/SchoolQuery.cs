using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using Colegios.Query.Core.QueryInterfaces;
using SharpArch.NHibernate;
using NHibernate.Criterion;
using Colegios.Query.Core.Criteria;
using NHibernate;
using log4net;
using NHibernate.Dialect.Function;

namespace Colegios.Query
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class SchoolQuery: NHibernateQuery, ISchoolQuery
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SchoolQuery));

        public IEnumerable<SchoolDto> FindSchools(SchoolCriteria criteria, int pageSize, out int totalCount, out IList<HighLightSchoolDto> highLightedSchools, int highLightedNum, int pageNo = 1)
        {
            highLightedSchools = new List<HighLightSchoolDto>();
            var pastHighLightedIds = 0;
            ISQLFunction sqlAdd = new VarArgsSQLFunction("(", "+", ")");
            var query2 = Session.QueryOver<SchoolDto>()
                .SelectList(lists => lists.Select(Projections.SqlFunction(sqlAdd, NHibernateUtil.Int64, Projections.SqlFunction(sqlAdd, NHibernateUtil.Int64, Projections.Constant(10), Projections.Constant(20)), Projections.Constant(19)))).List<Int64>();

            //Array lstLocalities = localities.ToArray<int>();
            if (criteria.Localities.Any())
            {

                log.Info("SchoolQuery find schools with localities: " + criteria.Localities);

                totalCount = 0;

                var query = Session.QueryOver<SchoolDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);

                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.isBilingue == criteria.IsBilingue);

                if (criteria.NoBreak)
                    query = query.Where(x => x.ContinuousTime == criteria.NoBreak);

                if (criteria.Localities.Count() == 1)
                {
                    HighLightSchoolDto highLightSchool = null;
                    var hightLightedQuery = Session.QueryOver<HighLightSchoolDto>(() => highLightSchool).Where(x => x.StartDate <= DateTime.Now).And(x => x.EndDate >= DateTime.Now)
                                            .JoinQueryOver(x => x.School).Where(x=> x.Locality.Id == criteria.Localities.First());

                    if (criteria.EducationType != null)
                        hightLightedQuery = hightLightedQuery.Where(x => x.Type.Id == criteria.EducationType);

                    if (!string.IsNullOrEmpty(criteria.Keywords))
                        hightLightedQuery = hightLightedQuery.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                    if (criteria.IsBilingue)
                        hightLightedQuery = hightLightedQuery.Where(x => x.isBilingue == criteria.IsBilingue);

                    if (criteria.NoBreak)
                        hightLightedQuery = hightLightedQuery.Where(x => x.ContinuousTime == criteria.NoBreak);

                    highLightedSchools = hightLightedQuery.OrderBy(() => highLightSchool.StartDate).Asc.List();
                }

                var highLightedIds = highLightedSchools.Select(x => x.School.Id).ToList();

                totalCount = query
                            .AndRestrictionOn(x => x.Locality.Id).IsIn(criteria.Localities)
                            .Select(Projections.RowCount()).SingleOrDefault<int>();

                query = Session.QueryOver<SchoolDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);
                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.isBilingue == criteria.IsBilingue);

                if (criteria.NoBreak)
                    query = query.Where(x => x.ContinuousTime == criteria.NoBreak);

                if (highLightedSchools.Any())
                {
                    var hqlHighLighted = "select school.Id from Schools school "
                                        + " inner join lookup_localities locality on school.localityId = locality.Id ";

                    var conditions = new List<string>();
                    conditions.Add(" locality.Id = " + criteria.Localities.First());
                    if (criteria.EducationType != null)
                        conditions.Add(" school.type=" + criteria.EducationType);
                    if (!string.IsNullOrEmpty(criteria.Keywords))
                        conditions.Add(" school.name like '%" + criteria.Keywords + "%'");
                    if (criteria.IsBilingue)
                        conditions.Add(" school.Bilingue=" + criteria.IsBilingue);
                    if (criteria.NoBreak)
                        conditions.Add(" school.continuousTime=" + criteria.NoBreak);

                    hqlHighLighted += " where ";

                    for (var index = 0; index < conditions.Count(); index++)
                    {
                        if (index == 0)
                            hqlHighLighted += conditions[index];
                        else hqlHighLighted += " and " + conditions[index];
                    }

                    hqlHighLighted += " order by school.Name asc limit 0," + ((pageNo - 1) * pageSize);


                    pastHighLightedIds = Session.CreateSQLQuery(hqlHighLighted).List<int>().Intersect<int>(highLightedIds).Count();
                }

                var schools = query
                        .AndRestrictionOn(x => x.Locality.Id).IsIn(criteria.Localities)
                        .OrderBy (x=> x.Name).Asc()
                        .Skip(((pageNo - 1) * pageSize) + pastHighLightedIds).Take(pageSize + highLightedNum)
                        .List();

                return schools.Except(highLightedSchools.Select(x => x.School)).Take(pageSize);
            }
            else //looking by townid
            {

                log.Info("SchoolQuery find schools without localities");

                totalCount = 0;

                var query = Session.QueryOver<SchoolDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);

                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.isBilingue == criteria.IsBilingue);

                if (criteria.NoBreak)
                    query = query.Where(x => x.ContinuousTime == criteria.NoBreak);

                totalCount = query
                            .JoinQueryOver(x => x.Locality).Where(x => x.Town.Id == criteria.TownId)
                            .Select(Projections.RowCount()).SingleOrDefault<int>();


                query = Session.QueryOver<SchoolDto>();
                if (criteria.EducationType != null)
                    query = query.Where(x => x.Type.Id == criteria.EducationType);
                if (!string.IsNullOrEmpty(criteria.Keywords))
                    query = query.Where(x => x.Name.IsLike("%" + criteria.Keywords + "%"));

                if (criteria.IsBilingue)
                    query = query.Where(x => x.isBilingue == criteria.IsBilingue);

                if (criteria.NoBreak)
                    query = query.Where(x => x.ContinuousTime == criteria.NoBreak);

                return query
                              .JoinQueryOver(x=> x.Locality).Where (x=> x.Town.Id == criteria.TownId)
                              .OrderBy(x=> x.Name).Asc()
                              .Skip((pageNo - 1)*pageSize).Take(pageSize)
                              .List();
                              //.And(x => x.Town.Id == townId).List();
            }
        }
       
        public SchoolDto GetSchool(int schoolId)
        {
            //Load and Get are Hibernate methods to retrieve an entity based on its Id
            //Load retrives a proxy to object or an exception, it should be used when a object certainly exist
            //Get retrieves null otherwise retrives the instance
            return Session.Load<SchoolDto>(schoolId);
        }

        public IEnumerable<SchoolDto> Ranking(int year, int markType, int? tipoCentro, int townId, int[] localities, out int numTotalSchools, int pageNo = 1, int pageSize = 50)
        {
            //cdi
            if (markType == 1)
                return RankingCdi(year, tipoCentro, townId, localities, out numTotalSchools, pageNo, pageSize);
            else //lea type
                return RankingLea(year, tipoCentro, townId, localities, out numTotalSchools, pageNo, pageSize);

        }

        private IEnumerable<SchoolDto> RankingCdi(int year, int ? tipoCentro, int townId, int[] localities, out int numTotalSchools, int pageNo=1, int pageSize=50)
        {
            SchoolDto school = null;
            LocalityDto locality = null;
            IList<MarkDto> marks = new List<MarkDto>();
            var query = Session.QueryOver<SchoolDto>(() => school);
            var queryCount = Session.QueryOver<SchoolDto>(() => school);

            if (tipoCentro != null)
            {
                queryCount = queryCount.Where(x => x.Type.Id == (int)tipoCentro);
                query = query.Where(x => x.Type.Id == (int)tipoCentro);
            }

            if (localities.Any())
            {
                numTotalSchools = queryCount.AndRestrictionOn(x => x.Locality.Id).IsIn(localities)
                    .JoinQueryOver<MarkDto>(x => school.Marks).Where(y => y.YearExam == year).Select(Projections.RowCount()).SingleOrDefault<int>();

                return query.AndRestrictionOn(x => x.Locality.Id).IsIn(localities)
                    .JoinQueryOver<MarkDto>(x => school.Marks).Where(y => y.YearExam == year).OrderBy(x => x.AverageMark).Desc.Skip((pageNo -1) * pageSize).Take(pageSize).List();
            }else {

                numTotalSchools = Session.QueryOver<SchoolDto>(() => school).JoinQueryOver(x => x.Locality, () => locality).Where(x => x.Town.Id == townId)
                    .JoinQueryOver<MarkDto>(x => school.Marks).Where(y => y.YearExam == year).Select(Projections.RowCount()).SingleOrDefault<int>();

                return query.JoinQueryOver(x => x.Locality, () => locality).Where(x => x.Town.Id == townId)
                    .JoinQueryOver<MarkDto>(x => school.Marks).Where(y => y.YearExam == year).OrderBy(x => x.AverageMark).Desc.Skip((pageNo -1) * pageSize).Take(pageSize).List();
                    //.OrderBy(x => marks.First().AverageMark).Desc.List();
            }

        }


        private IEnumerable<SchoolDto> RankingLea(int year, int? tipoCentro, int townId, int[] localities, out int numTotalSchools, int pageNo = 1, int pageSize = 50)
        {
            SchoolDto school = null;
            LocalityDto locality = null;
            IList<MarkLeaDto> marks = new List<MarkLeaDto>();
            var query = Session.QueryOver<SchoolDto>(() => school);
            var queryCount = Session.QueryOver<SchoolDto>(() => school);

            if (tipoCentro != null)
            {
                queryCount = queryCount.Where(x => x.Type.Id == (int)tipoCentro);
                query = query.Where(x => x.Type.Id == (int)tipoCentro);
            }

            if (localities.Any())
            {
                numTotalSchools = queryCount.AndRestrictionOn(x => x.Locality.Id).IsIn(localities)
                    .JoinQueryOver<MarkLeaDto>(x => school.MarksLea).Where(y => y.YearExam == year).Select(Projections.RowCount()).SingleOrDefault<int>();

                return query.AndRestrictionOn(x => x.Locality.Id).IsIn(localities)
                    .JoinQueryOver<MarkLeaDto>(x => school.MarksLea).Where(y => y.YearExam == year).OrderBy(x => x.TotalAverageMark).Desc.Skip((pageNo - 1) * pageSize).Take(pageSize).List();
            }
            else
            {

                numTotalSchools = Session.QueryOver<SchoolDto>(() => school).JoinQueryOver(x => x.Locality, () => locality).Where(x => x.Town.Id == townId)
                    .JoinQueryOver<MarkLeaDto>(x => school.MarksLea).Where(y => y.YearExam == year).Select(Projections.RowCount()).SingleOrDefault<int>();

                return query.JoinQueryOver(x => x.Locality, () => locality).Where(x => x.Town.Id == townId)
                    .JoinQueryOver<MarkLeaDto>(x => school.MarksLea).Where(y => y.YearExam == year).OrderBy(x => x.TotalAverageMark).Desc.Skip((pageNo - 1) * pageSize).Take(pageSize).List();
                //.OrderBy(x => marks.First().AverageMark).Desc.List();
            }

        }

        public IEnumerable<SchoolDto> DistrictSchools(int localityId)
        {
            return
                Session.QueryOver<SchoolDto>().JoinQueryOver(x => x.Locality).Where (x=> x.Id == localityId)
                    .List();
        }
    }
}