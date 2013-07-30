using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.ApplicationServices.Interfaces;
using Colegios.Command.Data;
using Colegios.Command.Core.Domain;
using Colegios.Command.Core.DataInterfaces;


namespace Colegios.ApplicationServices
{
    public class NurseryService : INurseryService
    {
        private LocalityRepository localityRepository;
        private CategoryFieldRepository categoryFieldRepository;
        private NurseryModifiedRepository nurseryModifiedRepository;

        public NurseryService(LocalityRepository localityRepository, CategoryFieldRepository categoryFieldRepository, INurseryModifiedRepository nurseryModifiedRepository, IUserRepository userRepository)
        {
            this.localityRepository = localityRepository;
            this.categoryFieldRepository = categoryFieldRepository;
            
            this.nurseryModifiedRepository = (NurseryModifiedRepository)nurseryModifiedRepository;
        }

        public int NurserySignUp(NurseryModified nursery, int townId, int districtId, IDictionary<int, string> categoryValues)
        {

            using (nurseryModifiedRepository.DbContext.BeginTransaction())
            {
                try
                {
                    nursery.Locality = localityRepository.Get(districtId);

                    var nurseryDetails = new List<NurseryDetailsModified>();

                    foreach (var categoryFieldId in categoryValues.Keys)
                    {
                        var category = categoryFieldRepository.Get(categoryFieldId);
                        var value = categoryValues[categoryFieldId];
                        nurseryDetails.Add(new NurseryDetailsModified() { CategoryField = category, Value = value, Nursery = nursery });
                    }
                    nursery.Details = nurseryDetails;
                    nursery.User = nursery.User;
                    nurseryModifiedRepository.Save(nursery);
                    MailManager.SendMail(nursery.User.Email, "contacto@colesyguardes.es", string.Empty, "Confirmación de registro en colesyguardes.es", "<html><body><p>Confirmation e-mail</p></body></html>");
                    nurseryModifiedRepository.DbContext.CommitTransaction();
                    return nursery.Id;
                }
                catch (Exception e)
                {
                    nurseryModifiedRepository.DbContext.RollbackTransaction();
                    return -1;
                }
            }

        }
    }
}
