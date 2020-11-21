using sveikata.DTOs;

namespace sveikata.Services.Responses
{
    public class DiseaseResponse : BaseResponse
    {
        public DiseaseDTO disease { get; set; }

        public DiseaseResponse(DiseaseDTO disease) : base(string.Empty, true)
        {
            this.disease = disease;
        }

        public DiseaseResponse() : base(string.Empty, true)
        {
        }

        public DiseaseResponse(string message) : base(message, false)
        {
        }
    }
}
