using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;
using MessagePack;

namespace AddressService.Domain;
[Table("Address"), MessagePackObject(keyAsPropertyName: true)]

public class Address : BaseEntity
{

    [Column("addressAddress")]
    public Guid Address { get; set; }

    [Column("street")] public Guid Street { get; set; }

    [Column("streetNr")] public Guid StreetNr { get; set; }

    [Column("zipcode")] public Guid Zipcode { get; set; }

    [Column("longitude")] public Guid Longitude { get; set; }

    [Column("latitude")] public Guid Latitude { get; set; }

    public AddressStatus AddressStatus { get; set; }




}