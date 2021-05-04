﻿using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.HouseDTO
{
    public class HouseDTO: IProfileBase
    {
        public Guid Id { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public HouseType Type { get; set; }

        public Guid? UserId { get; }

        public BuildingInfoDTO Info { get; }

        public ICollection<FlatDTO> Flats { get; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<House, HouseDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore());

            return config;
        }
    }
}
