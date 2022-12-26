using Microsoft.AspNetCore.Components;

namespace InnoGotchiGameFrontEnd.Presentation.Infrastructure
{
    public interface IElementReferenceService
    {
        public Task<string> GetInnerText(ElementReference element);
    }
}
