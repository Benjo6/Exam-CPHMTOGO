using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace AddressService.Domain;
[Table("Address"), MessagePackObject(keyAsPropertyName: true)]

public class Address : BaseEntity
{
    [Column("street")] public string Street { get; set; }

    [Column("streetNr")] public string StreetNr { get; set; }

    [Column("zipcode")] public string Zipcode { get; set; }

    [Column("longitude")] public double Longitude { get; set; }

    [Column("latitude")] public double Latitude { get; set; }
    
    [Column("etage")] public string? Etage { get; set; }
    [Column("door")]public string? Door { get; set; }




}