using Abp.Dapper.Dapper.Repositories;
namespace Abp.Dapper.Dapper
{
    public class NhBasedDapperAutoRepositoryTypes
    {
        static NhBasedDapperAutoRepositoryTypes()
        {
            Default = new DapperAutoRepositoryTypeAttribute(
                typeof(IDapperRepository<>),
                typeof(IDapperRepository<,>),
                typeof(DapperRepositoryBase<>),
                typeof(DapperRepositoryBase<,>)
                ); 
        }
        public static DapperAutoRepositoryTypeAttribute Default { get; }
    }
}
