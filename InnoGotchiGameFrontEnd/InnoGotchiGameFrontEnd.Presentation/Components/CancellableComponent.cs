using Microsoft.AspNetCore.Components;

namespace InnoGotchiGameFrontEnd.Presentation.Components
{
    public class CancellableComponent : ComponentBase, IDisposable
    {
        internal CancellationTokenSource _cts = new();
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
