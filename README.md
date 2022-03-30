# TelemetryStack
Offers a way to track data flow, errors, and changes through an application during development.

## Basic Introduction
TelemetryStack is a (currently) .Net Standard 2.0 C# library for detailed control of data and logic flow throughout your application during development. It's primarily aimed at more complicated program flow, particularly for deep object oriented style practices where it can be difficult to isolate logic errors and bring them to the surface.

The MethodTelemetry subclass is the primary working component, and at it's most basic, is meant to replace values returned by individual methods, as well as any exceptions, logic error tests, or notes. For example:
```
public int AddNumbers(int num1, int num2) {
  int returnObject;
  try {
    returnObject = num1 + num2;
  } catch (Exception ex)
  {
    Console.Writeline(ex.Message)
  }
  return returnObject;
}
```
would be:
```
public MethodTelemetry AddNumbers(int num1, int num2) {
  MethodTelemetry returnObject = new MethodTelemetry();
  returnObject.Message = "This method adds two integers to use in cost calculations";
  int result;
  try {
    result = num1 + num2;
  } catch (ExceptionError ex)
  {
    returnObject.ExceptionError = ex;
  }
  returnObject.Output = result;
  returnObject.Success = true;
  return returnObject;
}
```

The parent TelemetryStack class maintains an ordered list of all MethodTelemetry instances, up to a user set maximum. These can then be displayed or traversed as needed, including the values returned. This makes it easier to, say, find issues in a complicated back-end of a website where it can be difficult to walk a debugger all the way through, and display a cohesive rundown of exactly what was taking place at each step.

## Capabilities
The MethodTelemetry object automatically stores the output object Type in MethodTelemetry.OutputType when MethodTelemetry.Output is assigned. This allows for easy Type testing to ensure the MethodTelemetry object can be received and handled by any function.

Logic error tests can be written into the functions, the results of which can be held by the MethodTelemetry object. The MethodTelemetry object differentiates between exceptions and logic errors.

The name of the method that is returning the MethodTelemetry object is stored in the readonly MethodTelemetry.Sender attribute. This attribute is automatically assigned via a System.Diagnostics StackTrace.

The timestamp of the MethodTelemetry obect's initialization is stored in the readonly MethodTelemetry.Timestamp attribute to be used in calculating the time each method requires to complete, as well as overall logic branches.

## Conclusion
This class library is not intended to be a production library, and since I'm the only one using it at the moment, changes are currently only being made as necessity demands. However, it can be incredibly useful in getting more granularity in the information the developer has to work with while building applications that are operationally intensive.
