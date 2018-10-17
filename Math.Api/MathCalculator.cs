using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Api
{
  public class MathCalculator : IMathCalculator
  {
    public async Task<int> Add(int a, int b)
    {
      return a + b;
    }
  }
}
