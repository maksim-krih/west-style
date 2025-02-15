﻿using Loquimini.Model.Entities;
using Loquimini.ModelDTO.HouseDTO;
using System;
using System.Threading.Tasks;

namespace Loquimini.Service.Interfaces
{
    public interface IHouseService : IDisposable
    {
        Task<House> CreateHouseAsync(House house);
        Task<House> UpdateHouseAsync(HouseDTO houseDTO);
    }
}
