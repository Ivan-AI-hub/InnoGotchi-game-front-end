using Microsoft.AspNetCore.Components.Web;

namespace InnoGotchiGameFrontEnd.Presentation.Infrastructure
{
    public interface IMouseService
    {
        event EventHandler<MouseEventArgs>? OnMove;
        event EventHandler<MouseEventArgs>? OnUp;
        event EventHandler<MouseEventArgs>? OnLeave;
    }
}
