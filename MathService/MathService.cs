using Math.Api;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace MathService
{
  /// <summary>
  /// An instance of this class is created for each service instance by the Service Fabric runtime.
  /// </summary>
  internal sealed class MathService : StatelessService
  {
    private readonly IMathCalculator _mathCalculator_v2;
    private readonly ITextManipulator _textManipulator_v2;
    public MathService(StatelessServiceContext context)
        : base(context)
    {
      _mathCalculator_v2 = new MathCalculator();
      _textManipulator_v2 = new TextManipulator();
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
            _mathCalculator_v2,
            new FabricTransportRemotingListenerSettings()
            {
                EndpointResourceName = "MathCalculator_v2"
            }),
          "MathCalculator_v2"),
         new ServiceInstanceListener(
          context => new FabricTransportServiceRemotingListener(
            context,
            _textManipulator_v2,
            new FabricTransportRemotingListenerSettings()
            {
                EndpointResourceName = "TextManipulator_v2"
            }),
          "TextManipulator_v2")
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

        ServiceEventSource.Current.ServiceMessage(Context, "Math Service Working-{0}", ++iterations);

        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
      }
    }
  }
}
