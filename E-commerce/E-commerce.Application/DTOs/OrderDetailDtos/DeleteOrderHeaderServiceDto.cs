﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs.OrderDetailDtos
{
    public class DeleteOrderDetailServiceDto
    {
        public Guid Id { get; set; } // Nullable to allow for bulk deletion
    }
}
