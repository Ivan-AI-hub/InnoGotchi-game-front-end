using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    [Route("/ColaborationRequest")]
    public class ColaborationRequestController : BaseController
    {
        private ColaborationRequestManager _requestManager;
        private UserManager _userManager;
        private AuthorizeModel _authorizeModel;
        public ColaborationRequestController(ColaborationRequestManager requestManager, UserManager userManager, AuthorizeModel model)
        {
            _requestManager = requestManager;
            _userManager = userManager;
            _authorizeModel = model;
        }

        public async Task<IActionResult> UnconfirmedRequestsView()
        {
            foreach (var request in _authorizeModel.User.UnconfirmedRequest)
            {
                request.RequestSender = await _userManager.GetUserById(request.RequestSenderId);
                request.RequestReceiver = _authorizeModel.User;
            }
            return View(_authorizeModel.User.UnconfirmedRequest);
        }

        [Route("add")]
        public async Task<IActionResult> AddColaborator(int recipientId)
        {
            var rezult = await _requestManager.AddCollaborator(recipientId);
            var previousUrl = Request.Headers["Referer"].ToString();
            return Redirect(previousUrl);
        }

        [Route("{requestId}/confirm")]
        public async Task<IActionResult> ConfirmRequest(int requestId)
        {
            var rezult = await _requestManager.ConfirmRequest(requestId);
            var previousUrl = Request.Headers["Referer"].ToString();
            return Redirect(previousUrl);
        }

        [Route("{requestId}/reject")]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            var rezult = await _requestManager.RejectRequest(requestId);
            var previousUrl = Request.Headers["Referer"].ToString();
            return Redirect(previousUrl);
        }

        [Route("{requestId}/delete")]
        public async Task<IActionResult> DeleteById(int requestId)
        {
            var rezult = await _requestManager.DeleteById(requestId);
            var previousUrl = Request.Headers["Referer"].ToString();
            return Redirect(previousUrl);
        }
    }
}
