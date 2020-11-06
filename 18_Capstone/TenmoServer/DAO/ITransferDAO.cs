﻿using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<User> GetAllAccounts();

        Transfer SendMoneyTo(Transfer transfer);
    }
}