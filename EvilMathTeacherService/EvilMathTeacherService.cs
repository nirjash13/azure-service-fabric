using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EvilMathTeacher.Api;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V1.FabricTransport.Runtime;
//using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace EvilMathTeacherService
{
  /// <summary>
  /// An instance of this class is created for each service instance by the Service Fabric runtime.
  /// </summary>
  internal sealed class EvilMathTeacherService : StatelessService
  {
    private readonly IQuestionnaire _questionnaire;
    public EvilMathTeacherService(StatelessServiceContext context)
        : base(context)
    {
      _questionnaire = new Questionnaire();
    }

    /// <summary>
    /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
    /// </summary>
    /// <returns>A collection of listeners.</returns>
    protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
    {
      return new[]
     {
           new ServiceInstanceListener(
            context => new FabricTransportServiceRemotingListener(
              context,
              _questionnaire,
              new FabricTransportRemotingListenerSettings()
              {
                  EndpointResourceName = "Questionnaire_v2"

              }),
            "Questionnaire_v2")
       };
    }

    /// <summary>
    /// This is the main entry point for your service instance.
    /// </summary>
    /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
      // TODO: Replace the following sample code with your own logic 
      //       or remove this RunAsync override if it's not needed in your service.

      long iterations = 0;
      while (true)
      {
        cancellationToken.ThrowIfCancellationRequested();
        ServiceEventSource.Current.ServiceMessage(this.Context, "Evil teacher is coming to get you!-{0}", ++iterations);

        Random r = new Random();
        int first = r.Next(0, 100);
        var second = r.Next(200, 400);


        try
        {
          var sum = await _questionnaire.AddTwoNumbers(first, second);

          ServiceEventSource.Current.ServiceMessage(this.Context, "Evil Math teacher says - Sum-{0}", sum);

          var ifEqual = await _questionnaire.CheckIfTwoNumbersAreEqual(first, second);
          ServiceEventSource.Current.ServiceMessage(this.Context, "Evil Math teacher says - If Equal-{0}", ifEqual);

        }
        catch (Exception ex)
        {

          throw;
        }
        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
      }
    }
  }
}
