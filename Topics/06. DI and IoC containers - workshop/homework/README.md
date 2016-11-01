# DI and IoC containers Homework

*   Refactor the authors solution for the first task from the OOP exam (https://github.com/TelerikAcademy/Object-Oriented-Programming/tree/master/Topics/Exam-AuthorsSolutions/Dealership-AuthorSolution) utilizing the Dependency Inversion principle and an IoC container (i.e. Ninject).
  * Remove all uses of the **new** keyword (and replace them with corresponding code in an IoC container), so that all dependencies are injected or created via a factory (this includes the code in the factories themselves). You can only use the **new** keyword for creating internal types such as **StringBuilder**, **List**, **Exception** and etc.
  * Abstract all uses of concrete implementations such as the **Console** type and its methods. 
