using System;

namespace Core.Models.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }
    }
}