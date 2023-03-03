namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate
{
    public class ColaborationRequest
    {
        public int Id { get; set; }
        public ColaborationRequestStatus Status { get; set; }

        public int RequestSenderId { get; set; }
        public int RequestReceiverId { get; set; }
    }
}
