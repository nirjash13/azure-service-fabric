using Microsoft.ServiceFabric.Services.Remoting;
using System.Threading.Tasks;

namespace EvilMathTeacher.Api
{
  public interface IQuestionnaire : IService
  {
    Task<int> AddTwoNumbers(int a, int b);
    Task<bool> CheckIfTwoNumbersAreEqual(int a, int b);

    #region Publicly exposed
    Task<int> CallAddMethodFromOutsideClient(int a, int b);

    #endregion

  }
}
