﻿using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Api
{
  public interface ITextManipulator: IService
  {
    Task<bool> IfEqual(string a, string b);
  }
}
