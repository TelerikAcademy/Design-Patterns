**Important:** When downloading the project for evaluation, please consider that some problems might occur.
If there is an error when you are building or starting the project, please close it and delete the 'bin', 'obj', '.vs' and 'packages' folders. Open the solution again and the problem should be resolved. Please be patient and don't throw your colleagues work in the trash bin.  

If the full name of the CommandProcessor class is printed on the console when you start the application, ignore it :). It is caused by the Bytes2You validation library. Look in the Engine class in the author's solution. 

Part 1: Refactoring (35 points)

1. ModelsFactory class implements IModelsFactory.
    - Yes (1 point)
    - No (0 point)

2. Engine class implements IEngine.
    - Yes (1 point)
    - No (0 point)

3. Engine class receives ILogger (interface) instead of FileLogger (concrete class).
    - Yes (1 point)
    - No (0 point)

4. Engine class receives ICommandProcessor as external dependency instead of new CommandProcessor in the constructor.
    - Yes (1 point)
    - No (0 point)

5. The property Loogger is removed from the Engine class because you don't need it or the setter is removed.
    - Yes (1 point)
    - No (0 point)

6. The property Processor is removed from the Engine class because you don't need it or the setter is removed.
    - Yes (1 point)
    - No (0 point)

7. CommandProcessor class implements IProcessor.
    - Yes (1 point)
    - No (0 point)

8. CommandProcessor class receives ICommandsFactory (interface) instead of CommandsFactory (concrete class).
    - Yes (1 point)
    - No (0 point)

9. The property CommandFactory is removed from the CommandProcessor class because you don't need it or the setter is removed.
    - Yes (1 point)
    - No (0 point)

10. FileLogger implements ILogger.
    - Yes (1 point)
    - No (0 point)

11. ConsoleReader (or similar) is created and it implements IReader interface.
    - Yes (1 point)
    - No (0 point)

12. ConsoleWriter (or similar) is created and it implements IWriter interface.
    - Yes (1 point)
    - No (0 point)

13. Database is not singleton anymore.
    - Yes (1 point)
    - No (0 point)

14. Database is in SingletonScope in the NinjectManagerModule.
    - Yes (3 point)
    - No (0 point)

15. Abstract class Command receives IDatabase as external dependency instead of initialize it in the constructor.
    - Yes (3 point)
    - No (0 point)

16. The field Factory in CreationalCommand class is made private and there is property introduced to encapsulate it.
    - Yes (1 point)
    - No (0 point)

16. CreationalCommand class receives IModelsFactory (interface) instead of ModelsFactory (concrete class).
    - Yes (1 point)
    - No (0 point)

17. There is no switch-case in the CommandsFactory class and:
    - the Kernel is used to provide commands and it is setup in the NinjectManagerModule (author's solution) (7 points)
    - Service Locator is provided from another project with the Kernel inside it providing the commands (7 points)
    - the class is missing and the NinjectModule setups the Factory through the interface (auto-implemented factory) (7 points)
    - If the kernel is used and the switch-case is not removed. Just the methods invocation is replaced with kernel.Get<T>()  (1 point)
    - No (0 point)

18. The Engine class is bound in the NinjectManagerModule in SingletonScope.
    - Yes (2 points)
    - No (0 point)

19. The Validator, ConfigurationProvider, ConsoleReader, ConsoleWriter, FileLogger, CommandProcessor, CommandsFactory, ModelsFactory are bound in the NinjectManagerModule in SingletonScope.
    - Yes (3 points)
    - Most of them (2 points)
    - A few of them (1 point)
    - None of them (0 point)

20. In the StartUp class there is only one StandardKernel instance which receives or loads the needed module/s .
    - Yes (1 point)
    - No (0 point)

21. There is no new keyword in the NinjectManagerModule. Everything is instantiated through the Kernel even the constructor arguments.
    - Yes (1 points)
    - No (0 point)


Part 2: Unit Testing (30 points)
 
1. CacheableCommand Tests: Execute method should throw when parameters are null.
    - Yes (2 points)
    - No (0 point)

2. CacheableCommand Tests: If CachingService is expired Execute method should return internal command Execute method result and call ResetCache and AddCacheValue from CachingService.
    - Yes with mocked Internal command and CachingService (3 points)
    - No (even if the test passes if no mocking is used) (0 points)

3. CacheableCommand Tests: If CachingService is NOT expired Execute method should return CachedValue and call GetCacheValue from CachingService.
    - Yes with mocked Internal command and CachingService (3 points)
    - No (even if the test passes if no mocking is used) (0 points)

4. CachingService Tests: Constructor should throw when the duration is less than zero.
    - Yes (2 point)
    - No (0 point)

5. CachingService Tests: Constructor should set proper initial ExpiringDate.
    - Yes (with some kind of mocked DateTime: Ambient Context as an example) (1 points)
    - Yes (without mocked DateTime) (0.5 point)
    - No (0 point)

6. CachingService Tests: IsExpired returns true if TimeExpiring is before Now.
    - Yes (with some kind of mocked DateTime: Ambient Context as an example) (3 points)
    - Yes (without mocked DateTime) (0.5 point)
    - No (0 point)

7. CachingService Tests: IsExpired returns false if TimeExpiring is after Now.
    - Yes (with some kind of mocked DateTime: Ambient Context as an example) (2 points)
    - Yes (without mocked DateTime) (0.5 point)
    - No (0 point)

8. CachingService Tests: ResetCache should add proper value to timeExpiring.
    - Yes (with some kind of mocked DateTime: Ambient Context as an example) (3 points)
    - Yes (without mocked DateTime) (0.5 point)
    - No (0 point)

9. CachingService Tests: ResetCache should create new dictionary for cache.
    - Yes (1 point)
    - No (0 point)

10. LogErrorInterceptor Tests: Intercept calls Proceed() once.
    - Yes (2 point)
    - No (0 point)

11. LogErrorInterceptor Tests: Intercept when throws UserValidationException calls logger.Error() and writer.WriteLine() once.
    - Yes (2 point)
    - No (0 point)

12. LogErrorInterceptor Tests: Intercept when throws Exception calls logger.Fatal() and writer.WriteLine() once.
    - Yes (2 point)
    - No (0 point)

13. General: All the test using some kind of DateTime provider should call TearDown or reset it in some way.
    - Yes (1 point)
    - No (0 point)

14. General: Any other valid and adequate unit tests which are not in the above cases.
    - Many (3 point)
    - Some (2 point)
    - A few (1 point)
    - No (0 point)

Part 2: New Features (35 points)

1. ValidatableCommand decorator is implemented and it works as expected.
    - Yes (5 points)
    - Almost done (on the right track) (1 point)
    - No (0 point)
.
2. ValidatableCommand decorator is bound through Ninject in the NinjectManagerModule and decorates all the existing commands
    - Yes (5 points)
    - No (it is used with new somewhere in the project) (0 point)

3. CacheableCommand decorator is implemented and works as expected.
    - Yes (5 points)
    - Almost done (on the right track) (1 point)
    - No (0 point)

4. CacheableCommand decorator is bound through Ninject in the NinjectManagerModule and decorates ListProjects command.
    - Yes (5 points)
    - No (it is used with new somewhere in the project) (0 point)

5. There is implemented LogErrorInterceptor (It doesn't matter what is the exact name) which logs errors on the console and in the file. Basically it replaces the try-catch block in the Engine class.
    - Yes (5 points)
    - Almost done (on the right track) (2 points)
    - No (0 point)

6. CommandProcessor is Intercepted with the LogErrorInterceptor in the NinjectManagerModule.
    - Yes (3 points)
    - No (0 point)

7. There is implemented InfoInterceptor (It doesn't matter what is the exact name) which writes the invocation result on the console. It uses SimpleInterceptor and AfterInvoke method.
    - Yes (4 points)
    - This functionality is implemented in the LogErrorInterceptor (1 point)
    - No (0 point)

8. CommandProcessor is Intercepted with the InfoInterceptor in the NinjectManagerModule.
    - Yes (3 points)
    - No (0 point)