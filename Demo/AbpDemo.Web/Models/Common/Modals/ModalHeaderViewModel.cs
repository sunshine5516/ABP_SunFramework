namespace AbpDemo.Web.Models.Common.Modals
{
    public class ModalHeaderViewModel
    {
        public string Title { get; set; }
        public ModalHeaderViewModel(string title)
        {
            this.Title = title;
        }
    }
}