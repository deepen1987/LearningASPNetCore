using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ModelBindingValidationExample.Models;

namespace ModelBindingValidationExample.CustomModelBinders
{
    public class BookBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Books))
            {
                return new BinderTypeModelBinder(typeof(BookModelBinder));
            }
            return null;
        }
    }
}
