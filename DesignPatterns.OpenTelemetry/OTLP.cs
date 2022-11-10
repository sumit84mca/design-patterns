using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;

namespace GettingStarted;

public class OTLP
{
    private static readonly ActivitySource MyActivitySource = new(
        "MyCompany.MyProduct.MyLibrary");

    public void StartTrace()
    {
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource("MyCompany.MyProduct.MyLibrary")
            .AddConsoleExporter()
            .Build();

        using (var activity = MyActivitySource.StartActivity("SayHello"))
        {
            activity?.SetTag("foo", 1);
            activity?.SetTag("bar", "Hello, World!");
            activity?.SetTag("baz", new int[] { 1, 2, 3 });
            activity?.SetStatus(ActivityStatusCode.Ok);
        }
        try
        {
            throw new Exception("Testing exception");
        }
        catch
        {
            Console.WriteLine("Exception has been captured catch block");
        }
        finally
        {
            Console.WriteLine("Exception has been captured finally block");
        }
    }
}