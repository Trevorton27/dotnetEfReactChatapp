using DotnetEFReactChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetEFReactChatApp.Services
{
    public interface IAuthorizeUserService
    { 

                User Create(User user);
   
                User GetByEmail(string email);

   
                User GetById(int id);
}       
}
