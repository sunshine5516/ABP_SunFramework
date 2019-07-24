using AbpFramework.BackgroundJobs;
using AbpFramework.Dependency;
using AbpFramework.Domain.Repositories;
using AbpFramework.Domain.Uow;
namespace AbpDemo.Core
{
    public class SendPrivateEmailJob : BackgroundJob<SendPrivateEmailJobArgs>, ITransientDependency
    {
        private readonly IRepository<AbpDemo.Core.Authorization.Users.User, long> _userRepository;

        public SendPrivateEmailJob(IRepository<AbpDemo.Core.Authorization.Users.User, long> userRepository)
        {
            _userRepository = userRepository;
        }

        [UnitOfWork]
        public override void Execute(SendPrivateEmailJobArgs args)
        {
            using (CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, args.TargetTenantId))
            {
                var user = _userRepository.FirstOrDefault(args.TargetUserId);
                if (user == null)
                {
                    Logger.WarnFormat("Unknown userId: {0}. Can not execute job!", args.TargetUserId);
                    return;
                }

                //Here, we should actually send the email! We can inject and use IEmailSender for example.
                Logger.Info("Sending email to " + user.EmailAddress + " in background job -> " + args.Subject);
                Logger.Info(args.Body);
            }

               
        }
    }
}
