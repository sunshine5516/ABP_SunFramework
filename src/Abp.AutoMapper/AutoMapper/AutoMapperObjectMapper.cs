using AutoMapper;
using IObjectMapper = AbpFramework.ObjectMapping.IObjectMapper;

namespace Abp.AutoMapper.AutoMapper
{
    public class AutoMapperObjectMapper : IObjectMapper
    {
        #region 声明实例
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        public AutoMapperObjectMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion
        #region 方法
        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
        #endregion

    }
}
