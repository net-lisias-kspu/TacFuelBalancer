using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TAC Fuel Balancer /L Unleashed")]
[assembly: AssemblyDescription("Fuel Balancer addon for Kerbal Space Program")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(TacFuelBalancer.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(TacFuelBalancer.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(TacFuelBalancer.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(TacFuelBalancer.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f5619801-e09b-4b76-83a3-2b1ab59c0c9f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyVersion("2.20.*")]

[assembly: AssemblyFileVersion(TacFuelBalancer.Version.Number)]
[assembly: AssemblyVersion(TacFuelBalancer.Version.Number)]
[assembly: KSPAssembly("TacFuelBalancer", TacFuelBalancer.Version.major, TacFuelBalancer.Version.minor)]
[assembly: KSPAssemblyDependency("KSPe", 2, 4)]
[assembly: KSPAssemblyDependency("KSPe.UI", 2, 4)]
