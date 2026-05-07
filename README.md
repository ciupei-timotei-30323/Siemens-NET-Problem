# Solid Principle Violated

## In ItemRepository.cs

### 1. Dependency Inversion Principle Violated

#### Problem :

- At _line 8_ the Repository tightly coupled itself to a List, when the Dependency Inversion Principle dictates
that a High Level Module should depend on Interfaces.

#### Fix

- Instead of depending on a concret List object, a data source is injected through the Constructor of the class.
For this Dependency Injection to work, a private readonly _context object of type IDataContext is added. This will allow for easy
exchanges of data sources in the future while also following the Dependency Inversion Principle.
- For this Dependency Injection to work, a private readonly _context object of type IDataContext is added. 
This abstraction allows to easily swap the in-memory list for an HttpDataContext that fetches data from the external API.


### 2. Open/Closed Principle Violated

#### Problem:

- At line 22, the Method `GetAllAsync()` returned only the items that are active, meaning that if a extension of the method
was needed in order to have say non-active items too, the method would have to modify existing code.
- Same with GetByIdAsync.
#### Fix:

- I chose to simply rename the method `GetAllActiveAsync()` & `GetByIdAsync()` in order to be open for extension without a need
to modify the existing code.

### 3. Single Responsability Principle Violated

#### Problem:

- At line 9 a property `_nextId` was initialized with value 1. Even though this property was never used I assume it would be used to write
out Id's for objects in the DB which would go against the responsability of the class to Read data (It implements the IItemReader interface)

#### Fix:

- I simply deleted the property.

## In ItemController.cs

### 1. Single Responsability Principle Violated

#### Problem:

- In method `GetAll()` instead of the method redirecting the bussines logic to a Service class, it implements 
reading from the Data Base, counting and computing averages of items. This should be implemented in a Service class thus
violating the Single Responsability Principle.

#### Fix:

- The Method should just call _itemService.GetAllActiveWithStatsAsync, a new method in the Service layer, to get the DTO that include the count, average and the Item List.

### 2. Dependency Inversion Principle Violated

#### Problem:

- Throughout the controller (lines 16, 24, 39, 43, 50), the code uses Console.WriteLine() for logging.
This tightly couples the controller to the concrete system console. According to Dependency Inversion PRinciple, 
high-level modules should depend on abstractions, not concrete implementations. 

#### Fix:

- I injected an ILogger<ItemController> through the constructor alongside IItemReader. Then I replaced all instances of Console.WriteLine() 
with _logger.LogInformation() or _logger.LogWarning(). This delegates the actual logging mechanism to the framework's dependency injection container.


### 3. Single Responsibility Principle Violated (Second Instance)

#### Problem:

- In the GetById(int id) method at line 41, there is explicit input validation logic (if (id <= 0)). A controller's primary responsibility is to route HTTP requests
and return HTTP responses. It should not be responsible for knowing or enforcing domain validation rules.

#### Fix:

- Remove the manual `if` check from the controller. Instead, push this validation down into the Service layer where business rules are evaluated.
A new class called ItemValidator was created to deal with this, and then injected using the Constructor in the COntroller class.

### 4. Open/Closed Principle Violated

#### Problem:

- In the GetAll() method at line 26, the HTTP response is shaped using an anonymous object (return Ok(new { Data = itemList, Statistics = new { ... } });).
If the required response format changes, or if another endpoint needs to return this exact same data structure, this specific controller method needs to be modified. 
Hence, this class is not closed for modification regarding its output contracts.

#### FIx:

- I created a Data Transfer Object (DTO) class, ItemCollectionDTO. The Service layer should populate this DTO, and the controller should simply 
return it: return Ok(responseDto);. This makes the contract reusable and open for extension without modifying the controller's routing logic.