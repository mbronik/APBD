using zad5.Models;

namespace zad5.Dto;

public class CountryDto
{
    public string Name { get; set; }

    public static CountryDto Map(Country country)
    {
        return new CountryDto()
        {
            Name = country.Name
        };
    }
}