﻿using BAL.Dto.EquipmentDtos;
using DAL.Data.Entities;
using FluentResults;
using System.Threading;

namespace BAL.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        public Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken);

        public Task SubstractFromTotalAmountOfEquipmentAsync(OrderLine orderLine, Equipment equipment, CancellationToken cancellationToken);
        public Task AddToTotalAmountOfEquipmentAsync(OrderLine orderLine, Equipment equipment, CancellationToken cancellationToken);
    }
}
