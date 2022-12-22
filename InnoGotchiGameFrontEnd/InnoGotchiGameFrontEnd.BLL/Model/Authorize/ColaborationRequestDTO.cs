namespace InnoGotchiGameFrontEnd.BLL.Model.Authorize
{
    public class ColaborationRequestDTO
    {
        public int Id { get; set; }
        public ColaborationRequestStatusDTO Status { get; set; }

        public int RequestSenderId { get; set; }
        public int RequestReceiverId { get; set; }

        public UserDTO RequestSender { get; set; }
        public UserDTO RequestReceiver { get; set; }
    }
}
