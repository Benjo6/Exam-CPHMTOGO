using System.ComponentModel.DataAnnotations;
using Core.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AddressService.Domain.Dto;


public class AddressDto : BaseEntityDto

{

		public string Street { get; set; }

		public string StreetNr { get; set; }

		public int ZipCode { get; set; }

		public float Longitude { get; set; }

		public float Latitude { get; set; }


}
