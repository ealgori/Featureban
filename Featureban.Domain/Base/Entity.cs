using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Domain.Base
{
    public abstract class Entity<T>
    {
        public T Id { get; }
        protected Entity(T id)
        {
            this.Id = id;
        }
    }
}
