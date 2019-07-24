namespace AbpDemo.Application.Configuration.Ui
{
    public class UiThemeInfo
    {
        public string Name { get; set; }
        public string CssClass { get; set; }
        public UiThemeInfo(string name,string cssClass)
        {
            Name = name;
            CssClass = cssClass;
        }
    }
}
