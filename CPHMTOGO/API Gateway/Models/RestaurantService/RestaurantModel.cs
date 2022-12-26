using Newtonsoft.Json;

namespace APIGateway.Models.RestaurantService;

public record RestaurantModel(string id, string name, string address, string loginInfoId, string cityId, int kontoNr,
    int regNr, int CVR, string role)
{
    public List<MenuModel> Menus { get; set; }

};

public record UpdateRestaurantModel(string id, string name, string address, string loginInfoId, string cityId,
    int kontoNr,
    int regNr);
public class CreateRestaurantModel
{
    public string name { get; init; }
    public string address { get; init; }
    public string loginInfoId { get; init; }
    public string cityId { get; init; }
    public int kontoNr { get; init; }
    public int regNr { get; init; }
    public int CVR { get; set; }
}


