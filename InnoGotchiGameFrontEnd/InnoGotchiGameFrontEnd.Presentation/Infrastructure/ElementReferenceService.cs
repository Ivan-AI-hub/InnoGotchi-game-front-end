using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace InnoGotchiGameFrontEnd.Presentation.Infrastructure
{
    public class ElementReferenceService : IElementReferenceService
    {
        private readonly IJSRuntime _jsRuntime;

        public ElementReferenceService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task<string> GetInnerText(ElementReference element)
        {
            return await _jsRuntime.InvokeAsync<string>("getInnerHTML", element);
        }
    }
}
