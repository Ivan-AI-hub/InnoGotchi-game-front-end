namespace InnoGotchiGameFrontEnd.BLL.Model
{
    public record PetViewDTO
    {
        public string BodyFileLink { get; set; }
        public string EyeFileLink { get; set; }
        public string NoseFileLink { get; set; }
        public string MouthFileLink { get; set; }
    }
}
