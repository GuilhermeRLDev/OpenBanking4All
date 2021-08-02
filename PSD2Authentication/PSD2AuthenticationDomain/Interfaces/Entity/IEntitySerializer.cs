using System;
using System.Collections.Generic;
using System.Text;

namespace PSD2AuthenticationDomain.Interfaces.Entity
{
    public interface IEntitySerializer
    {
        string GetEntityInsertCommand();
    }
}
