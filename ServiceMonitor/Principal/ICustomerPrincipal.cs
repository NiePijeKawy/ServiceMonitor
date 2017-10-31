using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ServiceMonitor.Principal
{
    public interface ICustomerPrincipal : IPrincipal
    {
        string Login { get; set; }
    }

    public class CustomerPrincipal : ICustomerPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomerPrincipal( string userLogin)
        {
            this.Identity = new GenericIdentity(userLogin);
        }

        public string Login
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }


    public class CustomerPrincipalSerializedModel
    {
        public string Login { get; set; }
    }
}