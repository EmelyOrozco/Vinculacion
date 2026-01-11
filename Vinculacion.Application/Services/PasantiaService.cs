using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.Application.Services
{
    public class PasantiaService: IPasantiaService
    {
        public PasantiaService()
        {
            
        }


        public async Task<decimal> GetPasantiasActivasFinalizadas(decimal pasantiaID)
        {
            return 1;
        }
    }
}
