using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Helpers
{
    public class RegisterHelper
    {
        private readonly IClientService _clientService;
        
        public RegisterHelper(IClientService clientService)
        {
            _clientService = clientService;
        }

        public Client? RegisterClient(string name, string email, string password)
        {
            Client? existingClient = _clientService.Get(email);
            if (existingClient != null) return null; // Email already in use

            // Create new client
            var newClient = new Client(0, name, email, password);
            
            // Add to service and return result
            return _clientService.Add(newClient);
        }
    }
}
