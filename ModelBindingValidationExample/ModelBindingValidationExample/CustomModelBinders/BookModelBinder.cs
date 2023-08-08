using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBindingValidationExample.Models;

namespace ModelBindingValidationExample.CustomModelBinders
{
    public class BookModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Books book = new Books();
            if (bindingContext.ValueProvider.GetValue("BookID").Count() > 0)
            {
                book.Comments = bindingContext.ValueProvider.GetValue("BookID").FirstValue;

                if(bindingContext.ValueProvider.GetValue("Author").Count() > 0) 
                {
                    book.Comments += bindingContext.ValueProvider.GetValue("Author").FirstValue;
                }
            }

            bindingContext.Result = ModelBindingResult.Success(book);
            return Task.CompletedTask;
        }
    }
}
