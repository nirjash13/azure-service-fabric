using Math.Api;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilMathTeacher.Api
{
  public class Questionnaire : IQuestionnaire
  {
    public async Task<int> AddTwoNumbers(int a, int b)
    {
      var uri = new Uri("fabric:/SampleSFV2/MathService");
      var proxyFactory = new ServiceProxyFactory((c) =>
      {
        var settings = new FabricTransportRemotingSettings();
        return new FabricTransportServiceRemotingClientFactory(settings);
      });

      var service = proxyFactory.CreateServiceProxy<IMathCalculator>(uri, listenerName: "MathCalculator_v2");

      return await service.Add(a, b);
    }
   
    public async Task<bool> CheckIfTwoNumbersAreEqual(int a, int b)
    {
      var text1 = a.ToString();
      var text2 = b.ToString();

      var uri = new Uri("fabric:/SampleSFV2/MathService");
      var proxyFactory = new ServiceProxyFactory((c) =>
      {
        var settings = new FabricTransportRemotingSettings();
        return new FabricTransportServiceRemotingClientFactory(settings);
      });

      var service = proxyFactory.CreateServiceProxy<ITextManipulator>(uri, listenerName: "TextManipulator_v2");

      return await service.IfEqual(text1, text2);
    }


    #region Call from outside sf

    public async Task<int> CallAddMethodFromOutsideClient(int a, int b)
    {
      return await AddTwoNumbers(a, b);
    }

    #endregion
  }
}
