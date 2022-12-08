using System.ComponentModel.DataAnnotations;
using Core.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AddressService.Domain.Dto;


public class AddressDto : BaseEntityDto

{

		public string street { get; set; }

		public string streetNr { get; set; }

		public int zipcode { get; set; }

		public float longitude { get; set; }

		public float latitude { get; set; }


}
