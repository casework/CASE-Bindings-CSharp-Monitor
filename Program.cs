using CT.CASE.Bindings;
using VDS.RDF;

// This is an example consumer of the CT.CASE.Bindings package that is available through GitHub or NuGet
// GitHub: https://github.com/ciphertechsolutions/CASE-Bindings-CSharp
// Nuget: https://www.nuget.org/packages/CipherTech.CASE.Bindings

// Create a dataset object that initalized the Graph() object in the RDF library
// Graph Documentation: https://dotnetrdf.org/docs/stable/api/VDS.RDF.Graph.html
var ds = new DataSet();

// Create objects and pass named arguments to set the strongly-typed property values.
// Note: these objects are added to the graph upon creation and cannot be modified after instantiation.

// Object references can be saved as local variables for use in passing to future objects
// to allow the creation of strongly-typed relationships
// Ontology documentation: https://ontology.caseontology.org/case/documentation/class-identityorganization.html
var org = ds.CreateOrganization(rdfIdentifier: UriFactory.Create("kb:organization-" + Guid.NewGuid().ToString()), name: "Cyber Domain Ontology");


// Can pass the previous object reference to create the relationship
// If a reference to the created object is not needed, the return value can be ignored
ds.CreateTool(rdfIdentifier: UriFactory.Create("kb:tool-" + Guid.NewGuid().ToString()), name: "CASE Bindings CSharp Monitor", creator: org );


var latLong = ds.CreateLatLongCoordinatesFacet(rdfIdentifier: UriFactory.Create("kb:latlong-" + Guid.NewGuid().ToString()), latitude: 42, longitude: 42);
ds.CreateLocation(rdfIdentifier: UriFactory.Create("kb:location-" + Guid.NewGuid().ToString()), hasFacet: new Facet[] { latLong });

// Write the Graph out to a file. This allows direct interaction with the dotNetRDF library 
// and the various writer classes that exist. https://dotnetrdf.org/docs/stable/user_guide/Writing-RDF.html

// Define an IO writer
using StreamWriter file = new("case.json");

// Write the graph to JSON and save it to the IO writer handler
new VDS.RDF.Writing.RdfJsonWriter().Save(ds.Graph, file);