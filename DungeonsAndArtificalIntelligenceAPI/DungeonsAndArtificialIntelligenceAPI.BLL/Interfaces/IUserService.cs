using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces
{
    public interface IUserService
    {
        string Login(LoginBindingModel loginBindingModel);
        void Register(RegisterBindingModel registerBindingModel);
    }
}
