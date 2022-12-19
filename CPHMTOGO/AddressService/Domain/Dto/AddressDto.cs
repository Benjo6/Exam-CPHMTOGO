using System.ComponentModel.DataAnnotations;
using Core.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AddressService.Domain.Dto;


public class AddressDto : BaseEntityDto

{

		public string Street { get; set; }

		public string StreetNr { get; set; }
		
		public string ZipCode { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }


}
