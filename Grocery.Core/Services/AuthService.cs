﻿using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password)) return client;
            return null;
        }
        public Client? Register(string name, string email, string password)
        {
            Client? existingClient = _clientService.Get(email);
            if (existingClient != null) return null; // Email already in use
            string hashedPassword = PasswordHelper.HashPassword(password);
            Client newClient = new Client(0, name, email, hashedPassword);
            return _clientService.Add(newClient);
        }
    }
}
