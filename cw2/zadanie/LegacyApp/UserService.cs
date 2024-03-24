using System;

namespace LegacyApp
{
    public class UserService
    {
        private const int MIN_AGE = 21;
        private const int MIN_CREDIT_LIMIT = 500;

        
        private readonly UserCreditService userCreditService;
        private readonly ClientRepository clientRepository;

        
        public UserService()
        {
            userCreditService = new UserCreditService();
            clientRepository = new ClientRepository();
        }

        public UserService(UserCreditService userCreditService, ClientRepository clientRepository)
        {
            this.userCreditService = userCreditService;
            this.clientRepository = clientRepository;
        }


        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (!email.Contains("@") && !email.Contains("."))
                return false;

            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            if (age < MIN_AGE)
                return false;

            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                
                case "ImportantClient":
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2;
                    user.CreditLimit = creditLimit;
                    
                    break;
                }
                
                default:
                {
                    user.HasCreditLimit = true;
                    
                    var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;

                    break;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < MIN_CREDIT_LIMIT)
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}