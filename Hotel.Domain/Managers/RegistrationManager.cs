using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class RegistrationManager
    {
        private IRegistrationRepository _registrationRepository;
        public RegistrationManager(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }


        public void AddRegistration(Registrationn registration, int customerId)
        {
            try
            {
                _registrationRepository.AddRegistration(registration,customerId);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("AddRegistration",ex);
            }
        }

        public IReadOnlyList<Member> GetRegistratedMembersForActivity(int customerId, int activityId)
        {
            try
            {
                return _registrationRepository.GetRegistratedMembersForActivity(customerId, activityId);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("GetRegistratedMembersForActivity",ex);
            }
        }
    }
}
