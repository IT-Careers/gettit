using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gettit.Web.Models.Utilities.Binding
{
    public class TagsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var result = bindingContext.ValueProvider.GetValue("Tags").Values[0]?.Split(",").ToList();

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
