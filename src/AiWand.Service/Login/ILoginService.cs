using System;
using System.Collections.Generic;
using System.Text;
using AiWand.Core.Domain;

namespace AiWand.Service.Login
{
    public interface ILoginService
    {
        User Login(User loginUser);

        User Regster(User registerUser);
    }
}
