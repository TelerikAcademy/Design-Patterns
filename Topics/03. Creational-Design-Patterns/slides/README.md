<!-- section start -->
<!-- attr: { id:'', class:'slide-title', showInPresentation:true, hasScriptWrapper:true } -->
# Creational Patterns
## Initializing and configuring classes and objects

<article class="signature">
	<p class="signature-course">High-Quality Code</p>
	<p class="signature-initiative">Telerik Software Academy</p>
	<a href="http://academy.telerik.com " class="signature-link">http://academy.telerik.com </a>
</article>


<!-- section start -->
<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Table of Contents
- [Singleton](./singleton-pattern)
- [Lazy evaluation](./lazy-init)
- [Simple Factory](./factory)
- [Factory Method](./factory-method)
- [Abstract Factory](./abstract-factory)



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Creational Patterns
- Deal with object creation mechanisms
- Trying to create objects in a manner suitable to the situation
- Composed of two dominant ideas
  - Encapsulating knowledge about which concrete classes the system uses
  - Hiding how instances of these concrete classes are created and combined



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
<!-- # Singleton pattern
## Working with a single object -->


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Singleton
- The Singleton class is a class that is guaranteed to have only a single instance and provides global point of acces to it
    - Some examples: window manager, file system, console, application logger, state object
- Sometimes Singleton is wrongly thought of as a global variable – it is not!

# When do we use Singleton?
- When different, disjoint parts of a system need to access a single object
- When the system requires global access to the object instance
- When instantiation of an object is expensive, so it would be preferable to reuse a single instance and load it lazily

<!-- attr: { showInPresentation: true, style: 'font-size: 0.8em' } -->
# Some appropriate use cases
- **Logger** - a single logger instance can handle the logging for the entire application
- **Application configurations object** - the application configuration is shared between multiple parts of the system
- **Mediator** object - a single object that handles interactions between different parts of the system
- **Cache** - an object that provides caching services for one or more parts of the system



<!-- attr: { showInPresentation:true, hasScriptWrapper: true, style: 'font-size: 0.75em' } -->
# _Example_: Singleton logger
```cs
public Logger
{
    // store and encapsulate the instance
    private static Logger instance;
    // private constructor ensures that
    // Logger cannot be instantiated from outside
    private Logger()
    { }
    // provide access to the single instance
    public static Logger Instance
    {
        get
        {
            // instantiate on the first access
            if(instance == null)
                instance = new Logger();

            return instance;
        }
    }
    public void Log(string log)
    {
        Console.WriteLine(log);
    }
}
```

<!-- attr: { style: 'font-size: 0.8em' } -->
# Singletons and thread safety
- Using a the above implementation can produce undesired behaviour in multithreaded environments. Suppose we have a couple of threads - `thread 1` and `thread 2`:

```csharp
public static Logger Instance
{
    get
    {
        if(instance == null)
            // thread 1 and 2 can both reach here at the same time
            // Logger will be instantiated 2 times
            // the threads might happen to use 2 different instances
            instance = new Logger();
        
        return instance;
    }
}
```

<!-- attr: { style: 'font-size: 0.75em' } -->
# Thread-safe Singleton
- Thread safety can be achieved through the [.NET locking mechanism](https://msdn.microsoft.com/en-us/library/c5kehkcz.aspx)
```csharp
public class Logger
{
    private static Logger instance;
    private static object lockObject = new object();
    private Logger() {}
    public get Logger Instance
    {
        get
        {
            if(instance == null)
            {
                lock(lockObject)
                {
                    if(instance == null)
                        instance = new Logger();
                }
            }
            return instance;
        }
    }
}
```

<!-- attr: { class:'slide-section demo', showInPresentation: true, hasScriptWrapper: true } -->
<!-- # Singleton pattern
## [Demo]() -->


<!-- attr: { style: 'font-size: 0.8em' } -->
# Problems with Singletons
- **Singletons violate the `Single Responsibility` principle** - they are responsible for at least 2 roles: creating themselves and fullfiling their other responsibilities
- **Use of Singletons might break the `Open/Closed` principle** - if a singleton doesn't allow inheritance, it's not **open**. If it allows inheritance, it no longer enforces the single instance rule.

<!-- attr: { showInPresentation: true, style: 'font-size: 0.8em' } -->
<!-- # Problems with Singletons -->
- **Singletons can create hard coupling** - directly refering a class/object name inside your code in a lot of places ties you to that class/object
- **Singletons can make testing harder**, because they often rely on static methods and properties


<!-- section start -->
<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Lazy Initialization
## Just-in-time instantiation to improve initial performance

<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.8em' } -->
# Lazy Evaluation
- Delaying creation of an object, calculation of a value, or other expensive process until the first time it is needed
- Real-world examples
  - When implementing Singleton
  - IoC containers - lazy dependency injection
  - `LINQ` in .NET(Query building)
  - Infinite scrolling in web pages
  - JavaScript `LiveNodeList`
  - Lazy script loading in JavaScript clients
  - Text editor displaying files in directory
  - In ORMs navigation properties are lazy loaded (called dynamic proxies)
- In .NET: `Lazy<T>` (value holder)

<!-- attr: { class:'slide-section demo', showInPresentation: true, hasScriptWrapper: true } -->
<!-- # Lazy Evaluation
## [Demo]() -->

<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
<!-- # Simple Factory
## Abstracting complex creation, looser coupling, flexibility -->

# Simple Factory
- **An object for creating other objects**
    - Function or object responsible for creation of objects that have varying type/class/interface
    - The created objects implement a common interface
    - The factory encapsulates the creation logic
- Simple Factory is widely used
- It's considered a relatively basic pattern

<!-- attr: { style: 'font-size: 0.8em' } -->
# Motivation
- **Factory pattern helps us improve cohesion**
    - Instead of having all objects create the objects they need themselves, they can now request an instance from the factory
- **Better abstraction/easier object creation**
    - Factories encapsulate creation logic - they objects that use factories need not concern themselves with creation detail
    - Complex creation is hidden from the calling code
- **Looser coupling and flexibility**
    - If a change is needed in the creation logic, normally a single change in the creation object/function is enough
    - If a new type of object is to be created, only the object/function should be changed


<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.8em' } -->
# Simple Factory
## The Noob Implementation

```cs
// Parameter can be string (e.g. from configuration file)
public Coffee GetCoffee(CoffeeType coffeeType)
{
    // Can also be implemented using dictionary
    switch (coffeeType)
    {
        case CoffeeType.Regular:
            // Can be subtype of Coffee
            return new Coffee(0, 150);
        case CoffeeType.Double:
            return new Coffee(0, 200);
        case CoffeeType.Cappuccino:
            return new Coffee(100, 100);
        case CoffeeType.Macchiato:
            return new Coffee(200, 100);
        default:
            throw new ArgumentException("Unknown type of coffee!");
    }
}
```


# Problems with the above
- The method returns a concrete class
    - We're still coupled to it - better option is to return an interface
- The above implementation is not following the open\closed principle
    - To add a new type, we need to add a new enumeration value and make changes to the method
- **Solutions:**
    - The factory should return abstract type - abstract class or interface
    - The factory function/method should be overrideable **OR**
    - The factory should support type registration

<!-- attr: { style: 'font-size: 0.75em', hasScriptWrapper: true } -->
# Factory with registration in JS
- Error handling is omitted for clarity
- Uses dictionary of functions to instantiate objects

```js
const factory = (function () {

    // creates a dictionary
    const registeredTypes = Object.create(null); 
    
    function register(typeId, providerFunction) {
        registeredTypes[typeId] = providerFunction;
    }

    function create(typeId, ...parameters) {
        return registeredTypes[typeId].apply(null, parameters);
    }

    return {
        register,
        create
    }
} ());
```

<!-- attr: { style: 'font-size: 0.75em', hasScriptWrapper: true } -->
# Usage

```js
factory.register('person', 
        (name, age) => ({ name, age }));
factory.register('square', 
        side => { side, area: side * side, perimeter: side * 4 });

console.log(factory.create('person', 'john snow', 20));
console.log(factory.create('square', 10));
```

<!-- attr: { style: 'font-size: 0.75em', hasScriptWrapper: true } -->
# Factory with Reflection

```cs
public class Factory
{
    private static IDictionary<string, Type> registeredTypes 
                        = new Dictionary<string, Type>();

    public static void Register<T>(string typeId)
    {
        var type = typeof(T);
        if(type.IsAbstract || type.IsInterface)
            throw new ArgumentException(
        "Cannot create abstract type " + type.Name);

        registeredTypes.Add(typeId, type);
    }

    public static T Create<T>(string id, params object[] parameters)
    {
        Type typeToCreate;
        if(!registeredTypes.TryGetValue(id, out typeToCreate))
            throw new NotSupportedException(
        "Type with id [" + id + "] is not registered.");

        return (T)Activator.CreateInstance(typeToCreate, parameters);
    }
}
```

# Usage

```cs
// register Banana type with string id "Banana"
Factory.Register<Banana>("Banana");

// create a Banana and return it as IFood
// Banana should implement IFood
Factory.Create<IFood>("Banana");

// create a banana
Factory.Create<Banana>("Banana");
```


<!-- attr: { class:'slide-section demo', showInPresentation: true, hasScriptWrapper: true } -->
<!-- # Simple Factories
## [Demo]() -->


<!-- section start -->
<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Factory Method
## Reusing functionality and letting subtypes instantiate objects


<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.9em' } -->
# Factory Method
- **An interface or base type is defined**
    - **Object creation is defered to subtypes - they decide which type to create and how to create it**
- Allows for multiple simple factory implementations
    - Future changes can be integrated by implementing an interface/inheriting a type without modifying the existing codebase
- Allows to reuse common functionality with different components



<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.8em' } -->
# Factory Method
## Document example

```cs
abstract class Document
{
    private List<Page> _pages = new List<Page>();
    public Document() { this.CreatePages(); }
    public List<Page> Pages { get { return _pages; } }
    public abstract void CreatePages();
}
class CV : Document
{
    public override void CreatePages() {
        Pages.Add(new SkillsPage(), new BioPage()); // ...
    }
}
class Report : Document
{
    public override void CreatePages() {
        Pages.Add(new ResultsPage, SummaryPage()); // ...
    }
}
```

<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.8em' } -->
# Factory Method
## M16 Rifle example
- Implementing an M16 rifle for a video game:

```cs
public class M16
{
    private Scope scope = new StandardScope();
    private Camouflage camo = new DesertCamo();

    public double Mass { /* return the mass of the rifle */ }

    public Point2D ShootAtTarget(Point2D targetPosition)
    {
        // Very complicated calculation taking account 
        // of lots of variables such as
        // scope accuracy and gun weight.
    }
}
```

<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.9em' } -->
# A change in requirements
- The code above is fine at first glance
- **Consider that case where a stealth night mission in the jungle must be implemented**
    - We need a rifle with different scope and camo
    - Copying the code and initializing the fields with other types is clunky, repeats code unnecessary and doesn't a way to reuse existing functionality
- **We can use a factory method here**

<!-- attr: { showInPresentation:true, hasScriptWrapper:true,style: 'font-size: 0.9em' } -->
# The `M16` class <br />with Factory Method

```cs
public abstract class M16
{
    private Scope scope = this.CreateScope();
    private Camouflage camo = this.CreateCamo();

    public double Mass { /* return the mass of the rifle */ }

    public void ShootAtTarget(Point3D targetPosition)
    {
        // Very complicated calculation taking account 
        // of lots of variables such as
        // scope accuracy and gun weight.
    }

    // use virtual methods if defaults are needed
    protected abstract Scope CreateScope();
    protected abstract Camouflage CreateCamo();
}
```

<!-- attr: { style: 'font-size: 0.9em' } -->
# The `JungleM16` Rifle
- **Extending the base type lets us reuse functionality with different components**
    - The different components here are the `scope` and `camouflage` of the gun

```cs
public class JungleM16 : M16
{
    protected override Scope CreateScope()
    {
        return new NightVisionScope();
    }

    protected override CreateCamo()
    {
        return new JungleCamo();
    }
}
```


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Factory Method Problems
- **Factory Method prohibits parallel hierarchies**
    - Those hierarchies can become coupled
    - A single change in one of the hierarchies might result in changes in the whole hierarchies
- **Some good practices**
    - Avoid deep inheritance chains - they are hard to debug, change and reason about
    - Composition over inheritance can be applied in some cases to reduce hierarchy depth

<!-- section start -->
<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Abstract Factory
## Create families of related objects through an abstract interface

<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.85em' } -->
# Abstract Factory
- **Defines an abstract interface for creation a family of related types of objects**
    - The created objects are returned as interface types or base types
    - Multiple factories can implement the abstract interface
- Example:
    - An abstract factory for UI elements - windows, scrollbars, buttons, grids
    - A concrete family of UI elements might be `Material theme elements`, `OSX-like elements`, `Gnome UI style elements`
    - Different factories create the elements for Windows, OSX and GnomeUI
    
<!-- attr: { style: 'font-size: 0.9em' } -->
# Pros of an Abstract Factory
- The parts of the application that use the created objects do not know about the concrete classes
    - Coupling is loose, introducing changes isn't as cumbersome
- If requirements change and new types of objects must be created, this requires addition of new classes and changes only in the abstract factory
    - Greater flexibility
    - Greater testability


<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic14.png" style="top:52.01%; left:47.61%; width:55.29%; z-index:-1" /> -->


<!-- attr: { showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.9em' } -->
# Abstract Factory – _Example_

```cs
interface IContinentFactory { // AbstractFactory
   Herbivore CreateHerbivore();
   Carnivore CreateCarnivore();
}
class AfricaFactory : IContinentFactory {
   public Herbivore CreateHerbivore() {
      return new Wildbeаst();
   }
   public Carnivore CreateCarnivore() {
      return new Lion(); // Constructor can be internal
   }
}
class AmericaFactory : IContinentFactory {
    public Herbivore CreateHerbivore() {
        return new Bison();
    }
    public Carnivore CreateCarnivore() {
        return new Wolf();
    }
}
```



<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Pizza example
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic15.png" style="top:12.25%; left:4.46%; width:61.71%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic16.png" style="top:25.57%; left:69.96%; width:36.17%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic17.png" style="top:48.19%; left:26.54%; width:27.33%; z-index:-1" /> -->

# Problems
- This pattern can introduce a lot of accidental complexity
    - Does your application really need an Abstract Factory?


<!-- section start -->
# Further study
- Composition over inheritance
- Lazy evaluation in C# and JavaScript
- Prototype pattern
- Fluent interfaces



<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
<!-- # Creational Patterns
## Questions? -->



<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Free Trainings @ Telerik Academy
- C# Programming @ Telerik Academy
    - csharpfundamentals.telerik.com
  - Telerik Software Academy
    - academy.telerik.com
  - Telerik Academy @ Facebook
    - facebook.com/TelerikAcademy
  - Telerik Software Academy Forums
    - forums.academy.telerik.com