﻿using System.Text.Json.Serialization;

namespace InnoGotchiGame.Application.Models
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string? PhotoFileLink { get; set; }

		public PetFarm? OwnPetFarm { get; set; }

		public IEnumerable<ColaborationRequest> UnconfirmedRequest { get; set; }
		public IEnumerable<PetFarm?> CollaboratedFarms { get; set; }

		public User()
		{
			UnconfirmedRequest = new List<ColaborationRequest>();
			CollaboratedFarms = new List<ColaborationRequest>();
		}
	}
}
