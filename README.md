# Quiddity Objective

## Quiddity is Alpha WIP

Quiddity is a framework for designing entities in the problem domain area of a software, in terms of Clean Architecture.
It aims at presenting an alternative to common DTO and ORM patterns by introducing live aggregating object printers and clever object communication which minimizes repetition and data transfer.
Quiddity leverages GraphQL together with ZiZZI object printing capabilities to offer maximum flexibility in usage of the entity domain and usecase design. 

Any entity in software can be characterized by:
- One or more aspects that describe certain things about the entity
- Possible mutations of the entity

Entities often are bundled together by various aspects in clusters, which can
- Create new entities
- Remove entities
- Filter entities

These assumptions are reflected in the basic structure of Quiddity.

With quiddity, you can declare an entity in your software like this:

```csharp
var user =
  new SimpleQuiddity("Anna", 
    new SimpleAspect("Location",  //Define one or multiple aspects
      new ZiBlock(                //Declare the information of this aspect
        new ZiProp("Street", "Success-Avenue"),
        new ZiProp("City", "Greentown")
      )
    )
  );
```

Once declared, you can access the entity in your usecases. If you need information of a certain aspect, you can now request it:

```csharp
var location =
  user.Aspect("Location")
    .Into(new { City = "", Street = "" } ); //define an anonymous object and Quiddity will fill the properties from the declaration.

Console.WriteLine("Anna is in " +
  user.Aspect("Location")
    .Into(new { City = "" } )
    .City
);
```
This might look unusual at first, when you are used to common DTO approaches. The advantage of this technique is, that once you have declared your entity, you have a single source from which you slice data in different ways just as you need it. You do not need to define mutiple DTO and services which fill them, instead you declare aspects and tailor them to the minimum necessary directly at the point in your software where you need them.

While the example above uses static data in the declaration, the real advantage using quiddity is when you declare the data flow from dynamic sources instead:

```csharp
var user =
  new SimpleQuiddity("Anna", 
    new SimpleAspect("Location",
      new ZiBlock(                
        new ZiProp("Coordinates", () => LocationServices.GetCoordinates().ToString()),
        new ZiProp("City", () => LocationServices.GetCity())
      )
    )
  );
```

This declaration will pull the information directly when needed.

```csharp
var location =
  user.Aspect("Location")
    .Into(new { Coordinates = "", City = "" } ); //define an anonymous object and Quiddity will fill the properties as they are declared.

Console.WriteLine(location.Coordinates); //will be Anna's current location.
```

Quiddity is clever enough to not generate information which is not requested:
```csharp
var user =
  new SimpleQuiddity("Anna", 
    new SimpleAspect("Location",
      new ZiBlock(
        new ZiProp("LastUpdate", () => LocationServices.LastUpdate),  
        new ZiProp("Coordinates", () => LocationServices.GetCoordinates().ToString()),
        new ZiProp("City", () => LocationServices.GetCity())
      )
    )
  );


var location =
  user.Aspect("Location")
    .Into(new { LastUpdate = DateTime.MinValue } ); //Coordinates and City will not be retrieved in this object fill.
```

You can not only let quiddity express aspects as anonymous objects, you can format the data into json, xml or other formats - using ZiZZi matters:

```csharp
var location =
  user.Aspect("Location")
    .Format(new JsonMatter()); Will give you JSON.

var location =
  user.Aspect("Location")
    .Format(new XMLMatter()); Will give you XML.

var location =
  user.Aspect("Location")
    .Format(new LoggingMatter()); Will log the state of Anna's location to the Console.
```
