﻿using Abp.Web.Mvc.Web.Mvc.Views;
namespace BackgroundJobAndNotificationsDemo.Web.Views
{
    public abstract class BackgroundJobAndNotificationsDemoWebViewPageBase : BackgroundJobAndNotificationsDemoWebViewPageBase<dynamic>
    {

    }

    public abstract class BackgroundJobAndNotificationsDemoWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BackgroundJobAndNotificationsDemoWebViewPageBase()
        {
            
        }
    }
}