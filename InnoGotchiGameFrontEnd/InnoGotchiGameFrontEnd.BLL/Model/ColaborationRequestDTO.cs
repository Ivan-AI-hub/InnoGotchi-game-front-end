namespace InnoGotchiGameFrontEnd.BLL.Model
{
    public class ColaborationRequestDTO
    {
        public int Id { get; set; }
        public ColaborationRequestStatusDTO Status { get; set; }

        public int RequestSenderId { get; set; }
        public int RequestReceiverId { get; set; }
    }
}
