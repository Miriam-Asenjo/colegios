using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using Colegios.Command.Core.Domain;
using Colegios.Command.Core.DataInterfaces;

namespace Colegios.Command.Data
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class NurseryModifiedRepository: NHibernateRepository<NurseryModified>, INurseryModifiedRepository 
    {
        private LocalityRepository localityRepository;
        private CategoryFieldRepository categoryFieldRepository;

        public NurseryModifiedRepository(LocalityRepository localityRepository, CategoryFieldRepository categoryFieldRepository)
        {
            this.localityRepository = localityRepository;
            
            this.categoryFieldRepository = categoryFieldRepository;
        }

        public void RegisterNursery(NurseryModified nursery, int townId, int districtId, IDictionary<int, string> categoryValues)
        {
            using (DbContext.BeginTransaction())
            {
                try
                {
                    nursery.Locality = localityRepository.Get(districtId);

                    var nurseryDetails = new List<NurseryDetailsModified>();
 
                    foreach (var categoryFieldId in categoryValues.Keys){
                        var category = categoryFieldRepository.Get(categoryFieldId);
                        var value = categoryValues[categoryFieldId];
                        nurseryDetails.Add(new NurseryDetailsModified() { CategoryField = category, Value = value, Nursery = nursery });
                    }
                    nursery.Details = nurseryDetails;
                    base.Save(nursery);
                    DbContext.CommitTransaction();
                }
                catch (Exception e)
                {
                    DbContext.RollbackTransaction();
                    
                }
            }
        }
    }
}
