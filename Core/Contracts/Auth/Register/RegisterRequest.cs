using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Auth.Register
{
    public record RegisterRequest(
        string Email,
        string Password,
        string FirstName,
        string LastName
    );
}
