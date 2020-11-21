using sveikata.DTOs;
using sveikata.Models;

namespace sveikata.Mappers
{
    public class DiseaseMapper
    {
        public static DiseaseDTO Map(Disease disease)
        {
            return new DiseaseDTO()
            {
                Id = disease.Id,
                Name = disease.Name,
                Description = disease.Description
            };
        }
        public static Disease Map(DiseaseDTO itemDTO)
        {
            return new Disease()
            {
                Id = itemDTO.Id,
                Name = itemDTO.Name,
                Description = itemDTO.Description
            };
        }
    }
}
