using Abp.Dapper.Dapper.Repositories;
using Abp.Dapper.Tests.Entities;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Abp.Dapper.Tests
{
    public class DapperRepository_Tests: DapperApplicationTestBase
    {
        #region 声明实例
        private readonly IDapperRepository<Product> _productDapperRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<ProductDetail> _productDetailRepository;
        private readonly IDapperRepository<ProductDetail> _productDetailDapperRepository;
        #endregion
        #region 构造函数
        public DapperRepository_Tests()
        {
            _productDapperRepository= Resolve<IDapperRepository<Product>>();
            _productRepository = Resolve<IRepository<Product>>();
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            _productDetailRepository = Resolve<IRepository<ProductDetail>>();
            _productDetailDapperRepository = Resolve<IDapperRepository<ProductDetail>>();
        }
        #endregion
        #region 方法
        [Fact]
        public void Dapper_Repository_Tests()
        {
            using (IUnitOfWorkCompleteHandle uow = _unitOfWorkManager.Begin())
            {
                _productDapperRepository.Insert(new Product("TShirt"));
                Product insertedProduct = _productDapperRepository.
                    GetAll(x => x.Name == "TShirt").FirstOrDefault();
                insertedProduct.ShouldNotBeNull();
            }
        }
        #endregion
    }
}
