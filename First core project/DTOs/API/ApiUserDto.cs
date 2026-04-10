using System;

namespace First_core_project.DTOs.API
{
    public class ApiUserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
    }
}