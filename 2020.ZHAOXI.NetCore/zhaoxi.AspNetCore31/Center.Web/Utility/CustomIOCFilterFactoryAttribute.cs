using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Web.Utility
{
    public class CustomIOCFilterFactoryAttribute : Attribute, IFilterFactory
    {
        private readonly Type _FilterType = null;

        public bool IsReusable => true;

        public CustomIOCFilterFactoryAttribute(Type type)
        {
            this._FilterType = type;
        }


        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return (IFilterMetadata)serviceProvider.GetService(this._FilterType);
        }
    }
}
