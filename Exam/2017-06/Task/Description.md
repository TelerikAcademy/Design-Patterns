![Telerik Academy](https://github.com/TelerikAcademy/Common/raw/master/logos/telerik-header-logo.png)

# Project Manager Tool 

## Design Patterns Exam, 15 June 2017

Refactor and complete the project management system.

---

### Application description

About a month ago a development team from your company started to refactor the Project Management System which you are about to use in order to enhance your team's productivity. Initially, the system has been developed by junior developers and after some time a few senior members joined the team. Later on, the team switched to another project so you are the one man band now. The code is in some odd state of having all the needed interfaces to complete refactoring the system but not all of them have been used. You know [**"THE BOOK"**](http://i.imgur.com/7gjnaQv.png), right? Your task is to complete the refactoring following all the principles you have learnt  (I hope :D ) so far and build the best project ever. 


Guidelines:
- There are 2 projects and 1 test project. One of them is the framework and the other is the client.
- Both projects are splitted as this is an essential part of decoupling your application. In fact, building the framework is the main focus of this application and it should be done using all the SOLID principles. This will allow you to disconnect the client from the framework.
- All the modules of your application should be interchangeable, so you should use Dependency Inversion, as you already know from the course.
- On the client side of the project, you will need to utilize an IoC container (**Ninject** in this case).
- In order to meet the performance, you will have to cache the projects. Once you have to list them, you will have to return cached version of the list (for 20 seconds).
- Last but not least, you will need to cover the system with **UNIT** tests.
- Some steps already done for you:
 - Extracted a new Framework project where you will build your framework's internals.
 - In your client IoC container is installed (**Ninject**) and there is added **ConfigurationProvider**, which will enable you to add code for logging/caching functionality (based on configuration in your **app.config**, in the client project file).


##### Input
The input is read from the console and consists of a sequence of commands, each on a single line. The input ends by the "Exit" command.

##### Output
The output is also the same and should be on the console. It should consist of the outputs from each of the commands from the input sequence. There is, however, a new functionality you have to implement. You should add caching when **ListProjects** command is executed.

---

- **Hint 1:** Check what's going on in the following files in the CLI project (the architect left some goodies) - **StartUp**, **IConfigurationProvider**, **ConfigurationProvider**, **NinjectManagerModule**, **App.config**.

- **Hint 2:** Check the files **CacheableCommand** and **ValidatableCommand** and think about how to use them according to the use case

- **Hint 3:** All the needed interfaces are there for you (thank me later).

- **Hint 4:** SimpleInterceptor for writing the result of **ProcessCommand**
---

### Application requirements
Follow the good object orientated programming and (especially) dependency inversion practices and principles. Modifying things that you are NOT allowed to modify will result in a penalty for `each violation`.

#### Problem 1. Code Refactoring (35 points)
Refactor the code in both Framework and ConsoleClient projects following the best practices introduced in the Design Patterns course:

- Do not modify **ModelsFactory class**. Don't try to use IoC container there.

- Replace all usages of the **new** keyword with a corresponding architectural approach (design pattern) in all the classes: (except internal .NET classes such as **StringBuilder**, **List**, **Exception** types and etc.).

- Remove the switch case in **CommandsFactory** class and use the IoC container. It is okay to depend on it in this class. (Service Locator - please, no :) )

- Use an IoC container to compose all of the refactored code and allow to decouple the CLI from the framework and, of course, to allow unit testing.

#### Problem 2. Unit Tests (30 points)
Design and fully implement **unit tests for**:

- **CacheableCommand class**
- **CachingService class**
- **LogErrorInterceptor class**

Any other code is **not required** to be tested. You should cover the **public** methods. Be sure to test all major execution scenarios + all interesting border cases. You can use `VSTT`, `VSTT v2` or `NUnit`. Where applicable your tests should use mocking with `JustMock` or `Moq`.

#### Problem 3. New Features (35 points)
Implement caching functionality for the **ListProjects** command. Look at the **CachingService class** and the **ConsoleClient** for ideas how to use it.  

- Remove the validation from the commands. Use another approach to achieve the validation in one place.

- You should implement error logging and printing info about the **CommandProcessor class**.

- Remove the **try-catch** block in the **Engine** class and think about how to use the IoC container to achieve the same functionality over the **CommandProcessor**
  - The **only** line which should replace the whole **try-catch** block should be this one:
    **this.processor.ProcessCommand(commandLine);**
  - You should not write or log here (except for the **exit** command).

![info](http://i.imgur.com/Uyts2c2.png)

- The **CachingService** is ready for you. Use it to cache the **ListProjects** command. If you have already done the validation, use this approach here as well.

- **INFO ABOUT CACHING:** When **ListProjects** is called the first time, it prints out the list of the projects already added and cache it, then every other call will return the cached version for **20 seconds** (look at **app.config** file in **ConsoleClient**). Even if you add another project and then call **ListProjects** again, it should return the cached version if the cache is not expired yet.

- **You are NOT allowed to change, or move in other projects, the IConfigurationProvider interface and ConfigurationProvider class**
---


### Zero tests

You can also find them in the `Tests` folder.

Notice that ListProjects is always returning all the added projects. 
After implementing caching functionality, you should see cached projects for 20 seconds (or the time you added in the app.config file).
The first test shows the difference.


#### 01. Input

```
CreateProject DeathStar 2016-1-1 2018-05-04 Active
ListProjects
CreateProject SomeOtherStarWarsStuff 2016-1-1 2018-05-04 Active
ListProjects
Exit
```

#### 01. Expected output (Without caching. Notice the listing after adding the second project!)

```
Successfully created a new project!
Name: DeathStar
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
  - This project has no users!
  Tasks:
  - This project has no tasks!
Successfully created a new project!
Name: DeathStar
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
  - This project has no users!
  Tasks:
  - This project has no tasks!
Name: SomeOtherStarWarsStuff
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
  - This project has no users!
  Tasks:
  - This project has no tasks!
Program terminated.
```

#### 01. Expected output (With caching. Notice the listing after adding the second project!)

```
Successfully created a new project!
Name: DeathStar
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
  - This project has no users!
  Tasks:
  - This project has no tasks!
Successfully created a new project!
Name: DeathStar
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
  - This project has no users!
  Tasks:
  - This project has no tasks!
Program terminated.
```


#### 02. Input

```
CreateProject DeathStar 2016-1-1 2018-05-04 Active
CreateUser 0 DarthVader sexybeast@darkside.com
CreateTask 0 0 BuildTheStar Pending
Exit
```

#### 02. Expected output

```
Successfully created a new project!
Successfully created a new user!
Successfully created a new task!
Program terminated.
```

#### 03. Input

```
CreateProject DeathStar 2016-1-1 2018-05-04 Active
CreateUser 0 DarthVader sexybeast@darkside.com
CreateTask 0 0 BuildTheStar Pending
CreateTask 0 0 SecureTheVents Pending
CreateTask 0 0 Dominate InProgress
CreateProject DeathStar2 2016-1-1 2018-05-04 Active
CreateUser 1 DarthVader sexybeast@darkside.com
CreateTask 1 0 BuildTheStar Pending
CreateTask 1 0 SecureTheVents Pending
CreateTask 1 0 Dominate InProgress
ListProjects
Exit
```

#### 03. Expected output

```
Successfully created a new project!
Successfully created a new user!
Successfully created a new task!
Successfully created a new task!
Successfully created a new task!
Successfully created a new project!
Successfully created a new user!
Successfully created a new task!
Successfully created a new task!
Successfully created a new task!
Name: DeathStar
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
    Username: DarthVader
    Email: sexybeast@darkside.com
  Tasks:
    Name: BuildTheStar
    Owner: DarthVader
    State: Pending
  -------------
    Name: SecureTheVents
    Owner: DarthVader
    State: Pending
  -------------
    Name: Dominate
    Owner: DarthVader
    State: InProgress
Name: DeathStar2
  Starting date: 2016-01-01
  Ending date: 2018-05-04
  State: Active
  Users:
    Username: DarthVader
    Email: sexybeast@darkside.com
  Tasks:
    Name: BuildTheStar
    Owner: DarthVader
    State: Pending
  -------------
    Name: SecureTheVents
    Owner: DarthVader
    State: Pending
  -------------
    Name: Dominate
    Owner: DarthVader
    State: InProgress
Program terminated.
```
