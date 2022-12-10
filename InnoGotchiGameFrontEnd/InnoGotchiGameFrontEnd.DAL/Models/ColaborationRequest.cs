namespace InnoGotchiGameFrontEnd.DAL.Models
{
    public class ColaborationRequest
    {
        public int Id { get; set; }
        public ColaborationRequestStatus Status { get; set; }

        public int RequestSenderId { get; set; }
        public int RequestReceiverId { get; set; }
    }
}
