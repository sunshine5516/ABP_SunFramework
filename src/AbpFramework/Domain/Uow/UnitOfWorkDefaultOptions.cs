﻿using AbpFramework.Application.Services;
using AbpFramework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AbpFramework.Domain.Uow
{
    public class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        public TransactionScopeOption Scope { get; set; }
        public bool IsTransactional { get; set; }
        public bool IsTransactionScopeAvailable { get; set; }
        public TimeSpan? Timeout { get; set; }
        public IsolationLevel? IsolationLevel { get; set; }
        private readonly List<DataFilterConfiguration> _filters;

        public IReadOnlyList<DataFilterConfiguration> Filters => _filters;

        public List<Func<Type, bool>> ConventionalUowSelectors { get; }
        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
            IsTransactionScopeAvailable = true;
            ConventionalUowSelectors = new List<Func<Type, bool>>
            {
                type => typeof(IRepository).IsAssignableFrom(type) ||
                        typeof(IApplicationService).IsAssignableFrom(type)
            };
        }

        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new Exception("There is already a filter with name: " + filterName);
            }
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));            
        }
    }
}
