using sveikata.DTOs;
using sveikata.Models;

namespace sveikata.Mappers
{
    public class ServiceMapper
    {
        public static ServiceDTO Map(Service item)
        {
            return new ServiceDTO()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
        }
        public static Service Map(ServiceDTO itemDTO)
        {
            return new Service()
            {
                Id = itemDTO.Id,
                Name = itemDTO.Name,
                Description = itemDTO.Description
            };
        }
    }
}
