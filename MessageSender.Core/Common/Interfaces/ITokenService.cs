﻿using NotificationMessageSender.Core.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMessageSender.Core.Common.Interfaces
{
    public interface ITokenService
    {
        public string Generate (UserEntity user);
    }
}
