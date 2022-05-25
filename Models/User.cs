using System;
using System.Collections.Generic;

namespace dotnet_backend
{
    public partial class User
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
    }

    public partial class UserDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
