﻿using System.Text.Json.Serialization;

namespace InnoGotchiGame.Application.Models
{
	public class Pet
	{
		public int Id { get; set; }

		public PetStatistic Statistic { get; set; }
		public PetView View { get; set; }
		public int FarmId { get; set; }

		public Pet()
		{
			Statistic = new PetStatistic();
			View = new PetView();
		}
	}
}
