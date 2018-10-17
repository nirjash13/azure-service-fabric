using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Api
{
  public class TextManipulator : ITextManipulator
  {
    public async Task<bool> IfEqual(string a, string b)
    {
      return a == b;
    }
  }
}
