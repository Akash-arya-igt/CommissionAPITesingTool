using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Web.Optimization;

namespace CommissionAPITestingTool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(DateTime?), new MyDateTimeModelBinder());
        }

        public class MyDateTimeModelBinder : DefaultModelBinder
        {
            public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                if (!string.IsNullOrEmpty(displayFormat) && value != null)
                {
                    DateTime date;
                    displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                    // use the format specified in the DisplayFormat attribute to parse the date
                    if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} is an invalid date format", value.AttemptedValue)
                        );
                    }
                }

                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}
