using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PSD2AuthenticationDomain.Interfaces.Entity
{
    public interface IParent : IEntity
    {
        void AddItem(IEntity entity);
    }
}
