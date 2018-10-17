using EvilMathTeacher.Api;
using Math.Api;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using System;
using System.Threading;
using System.Windows.Forms;
namespace SampleSFV2OutsideClient
{
  public partial class Form1 : Form
  {
    private CancellationToken _cancellationToken;
    private Uri _mathServiceUri = new Uri("fabric:/SampleSFV2/MathService");
    private Uri _evilMathServiceUri = new Uri("fabric:/SampleSFV2/EvilMathTeacherService");
    public Form1()
    {
      InitializeComponent();

      textBox_MathServiceUri.Text = _mathServiceUri.OriginalString;
      textBox_EvilMathTeacherUri.Text = _evilMathServiceUri.OriginalString;

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private async void button_AddNumbers_Click(object sender, EventArgs e)
    {
      //int number1 = int.Parse(textBox_Number1.Text);
      //int number2 = int.Parse(textBox_Number2.Text);

      //var uri = _evilMathServiceUri;

      int number1 = 10;
      int number2 = 100;

      Uri uri = new Uri("fabric:/SampleSFV2/EvilMathTeacherService");

      ServiceProxyFactory proxyFactory = new ServiceProxyFactory((c) =>
      {
        FabricTransportRemotingSettings settings = new FabricTransportRemotingSettings();
        return new FabricTransportServiceRemotingClientFactory(settings);
      });

      try
      {
        IQuestionnaire service = proxyFactory.CreateServiceProxy<IQuestionnaire>(uri, listenerName: "Questionnaire_v2");
        int result = await service.AddTwoNumbers(number1, number2);

        MessageBox.Show("Result= " + result);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private async void button_AddMathService_Click(object sender, EventArgs e)
    {
      int number1 = int.Parse(textBox_MathServiceNumber1.Text);
      int number2 = int.Parse(textBox_MathServiceNumber2.Text);

      //var uri = new Uri("fabric:/SampleSFV2/MathService");
      ServiceProxyFactory proxyFactory = new ServiceProxyFactory((c) =>
      {
        FabricTransportRemotingSettings settings = new FabricTransportRemotingSettings();
        return new FabricTransportServiceRemotingClientFactory(settings);
      });

      try
      {
        IMathCalculator service = proxyFactory.CreateServiceProxy<IMathCalculator>(_mathServiceUri, listenerName: "MathCalculator_v2");

        int result = await service.Add(number1, number2);

        MessageBox.Show("Result= " + result);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }

    }
  }
}
