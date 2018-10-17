using Microsoft.ServiceFabric.Services.Remoting;
using System.Threading.Tasks;

namespace Math.Api
{
  public interface IMathCalculator: IService
  {
    Task<int> Add(int a, int b);
  }
}
