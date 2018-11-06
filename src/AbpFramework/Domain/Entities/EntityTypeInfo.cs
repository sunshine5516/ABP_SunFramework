﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Domain.Entities
{
    public class EntityTypeInfo
    {
        public Type EntityType { get;private set; }
        public Type DeclaringType { get; private set; }
        public EntityTypeInfo(Type entityType, Type declaringType)
        {
            EntityType = entityType;
            DeclaringType = declaringType;
        }
    }
}
