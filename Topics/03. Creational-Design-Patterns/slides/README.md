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
- [Lazy initialization](./lazy-init)
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
<!-- # Singleton pattern -->
## [Demo]()


<!-- attr: { style: 'font-size: 0.8em' } -->
# Problems with Singletons
## TODO: need review
- **Singletons violate the `Single Responsibility` principle** - they are responsible for at least 2 roles: creating themselves and fullfiling their other responsibilities
- **Use of Singletons might break the `Open/Closed` principle** - if a singleton doesn't allow inheritance, it's not **open**. If it allows inheritance, it no longer enforces the single instance rule.

<!-- attr: { showInPresentation: true, style: 'font-size: 0.8em' } -->
<!-- # Problems with Singletons -->
## TODO: need review
- **Using Singletons in code can break the `Dependency Inversion` principle** - instead of receiving the object as a dependency, the object is from the global context
- **Singletons can create hard coupling** - directly refering a class/object name inside your code in a lot of places ties you to that class/object
- **Singletons can make testing harder**, because they often rely on static methods and properties


<!-- section start -->
<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Lazy Initialization
## Just-in-time instantiation to improve initial performance
## TODO
<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Lazy Initialization
- Tactic of delaying the creation of an object, the calculation of a value, or some other expensive process until the first time it is needed
  - A.k.a. Virtual Proxy or Lazy Load pattern
- Real-world examples
  - In ORMs navigation properties are lazy loaded (called dynamic proxies) (N+1)
  - When implementing Singleton
  - IoC containers
- In .NET: Lazy<T> (value holder)

<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic25.png" style="top:49.37%; left:76.72%; width:26.45%; z-index:-1" /> -->


<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
<!-- # Simple Factory
## TODO -->


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Simple Factory
- Encapsulates object creation logic
  - If we are making specific class selection logic changes, we make them in one place
  - We can hide complex object creation
- This is not a real **Pattern**
  - This is the preparation for the real Pattern
- It is used quite often
- Each time we add new type we need to modify the simple factory code


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Simple Factory Demo _Example_

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
            throw new ArgumentException();
    }
}
```

<!-- section start -->
<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Factory Method
## TODO


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Factory Method
- Objects are created by separate method(s)
- Produces objects as normal factory
  - Adds an interface to the simple factory
  - Add new factories and classes without breaking Open/Closed Principle
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic12.png" style="top:44.08%; left:11.81%; width:83.53%; z-index:-1" /> -->


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Factory Method – _Example_

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



<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Factory Method Demo _Example_
- Inheritance hierarchy gets deeper with coupling between concrete factories and classes
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic13.png" style="top:11.46%; left:3.14%; width:100.18%; z-index:-1" /> -->

<!-- section start -->
<!-- attr: { class: 'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Abstract Factory
## TODO

<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Abstract Factory
- Abstraction in object creation
  - Create a family of related objects
- The **Abstract** **Factory** **Pattern** defines interface for creating sets of linked objects
  - Without knowing their concrete classes
- Used in systems that are frequently changed
- Provides flexiblemechanism forreplacement ofdifferent sets
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic14.png" style="top:52.01%; left:47.61%; width:55.29%; z-index:-1" /> -->


<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Abstract Factory – _Example_

```cs
abstract class ContinentFactory { // AbstractFactory
   public abstract Herbivore CreateHerbivore();
   public abstract Carnivore CreateCarnivore();
}
class AfricaFactory : ContinentFactory {
   public override Herbivore CreateHerbivore() {
      return new Wildebeest();
   }
   public override Carnivore CreateCarnivore() {
      return new Lion(); // Constructor can be internal
   }
}
class AmericaFactory : ContinentFactory {
    public override Herbivore CreateHerbivore() {
        return new Bison();
    }
    public override Carnivore CreateCarnivore() {
        return new Wolf();
    }
}
```



<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Abstract Factory Demo _Example_
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic15.png" style="top:12.25%; left:4.46%; width:61.71%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic16.png" style="top:25.57%; left:69.96%; width:36.17%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic17.png" style="top:48.19%; left:26.54%; width:27.33%; z-index:-1" /> -->



<!-- section start -->
# Further study
- TODO



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
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic28.png" style="top:60.37%; left:92.39%; width:13.45%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic29.png" style="top:30.85%; left:68.14%; width:36.30%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic30.png" style="top:46.32%; left:95.14%; width:10.85%; z-index:-1" /> -->
<!-- <img class="slide-image" showInPresentation="true" src="imgs\pic31.png" style="top:13.00%; left:92.85%; width:13.01%; z-index:-1" /> -->