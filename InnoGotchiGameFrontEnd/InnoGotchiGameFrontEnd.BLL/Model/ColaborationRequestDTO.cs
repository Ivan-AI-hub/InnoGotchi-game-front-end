namespace InnoGotchiGameFrontEnd.BLL.Model
{
    public class ColaborationRequestDTO
    {
        public int Id { get; private set; }
        public ColaborationRequestStatusDTO Status { get; private set; }

        public int RequestSenderId { get; private set; }
        public int RequestReceiverId { get; private set; }

        public UserDTO RequestSender { get; private set; }
        public UserDTO RequestReceiver { get; private set; }

        public ColaborationRequestDTO(int id, ColaborationRequestStatusDTO status, UserDTO requestSender, UserDTO requestReceiver)
        {
            Id = id;
            Status = status;
            RequestSender = requestSender;
            RequestReceiver = requestReceiver;
            RequestSenderId = requestSender.Id;
            RequestReceiverId = requestReceiver.Id;
        }

        public ColaborationRequestDTO(int id, ColaborationRequestStatusDTO status, int requestSenderId, int requestReceiverId)
        {
            Id = id;
            Status = status;
            RequestSenderId = requestSenderId;
            RequestReceiverId = requestReceiverId;
        }

        public void SetSender(UserDTO sender)
        {
            RequestSender = sender;
            RequestSenderId = sender.Id;
        }
        public void SetReceiver(UserDTO receiver)
        {
            RequestReceiver = receiver;
            RequestReceiverId = receiver.Id;
        }
    }
}
