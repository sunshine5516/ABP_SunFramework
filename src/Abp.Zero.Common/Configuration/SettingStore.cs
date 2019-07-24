using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbpFramework.Configuration;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
namespace Abp.Zero.Common.Configuration
{
    public class SettingStore : ISettingStore, ITransientDependency
    {
        #region 声明实例
        private readonly IRepository<Setting, long> _settingRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion
        #region 构造函数
        public SettingStore(IRepository<Setting, long> settingRepository
            , IUnitOfWorkManager unitOfWorkManager)
        {
            _settingRepository = settingRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        #endregion
        #region 方法
        [UnitOfWork]
        public virtual async Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    return (await _settingRepository.GetAllListAsync(s => s.UserId == userId &&
                     s.TenantId == tenantId)).Select(s => s.ToSettingInfo()).ToList();
                }
            }

        }
        [UnitOfWork]
        public virtual async Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    return (await _settingRepository.FirstOrDefaultAsync(s => s.UserId == userId
                     && s.Name == name && s.TenantId == tenantId)).ToSettingInfo();
                }

            }
        }
        [UnitOfWork]
        public virtual async Task DeleteAsync(SettingInfo settingInfo)
        {
            using (_unitOfWorkManager.Current.SetTenantId(settingInfo.TenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    await _settingRepository.DeleteAsync(
                        s => s.UserId == settingInfo.UserId && s.Name == settingInfo.Name
                        && s.TenantId == settingInfo.TenantId);
                }
            }
        }
        [UnitOfWork]
        public virtual async Task CreateAsync(SettingInfo settingInfo)
        {
            using (_unitOfWorkManager.Current.SetTenantId(settingInfo.TenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    await _settingRepository.InsertAsync(settingInfo.ToSetting());
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                }
            }
        }
        [UnitOfWork]
        public virtual async Task UpdateAsync(SettingInfo settingInfo)
        {
            using (_unitOfWorkManager.Current.SetTenantId(settingInfo.TenantId))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var setting = await _settingRepository.FirstOrDefaultAsync(
                        s => s.TenantId == settingInfo.TenantId &&
                        s.UserId == settingInfo.UserId &&
                        s.Name == settingInfo.Name
                        );
                    if(setting!=null)
                    {
                        setting.Value = settingInfo.Value;
                    }
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                }
            }
        }
        #endregion
    }
}
