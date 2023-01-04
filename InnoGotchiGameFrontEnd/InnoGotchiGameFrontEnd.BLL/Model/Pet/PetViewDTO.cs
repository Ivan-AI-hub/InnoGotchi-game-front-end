﻿namespace InnoGotchiGameFrontEnd.BLL.Model.Pet
{
    public record PetViewDTO
    {
        public PictureDTO? BodyPicture { get; set; }
        public PictureDTO? EyePicture { get; set; }
        public PictureDTO? NosePicture { get; set; }
        public PictureDTO? MouthPicture { get; set; }
    }
}