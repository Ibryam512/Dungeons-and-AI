﻿using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces
{
    public interface IUserService
    {
        LoginResponeViewModel Login(LoginBindingModel loginBindingModel);
        void Register(RegisterBindingModel registerBindingModel);
    }
}
