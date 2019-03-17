# Code Style

## Bracing
* Open braces should always be at the beginning of the line after the statement that begins the block
* Contents of the brace should be indented by 4 spaces
* Braces should never be considered optional

### Example

* Yes
```csharp
if (someExpression)
{
    DoSomething();
}
else
{
    DoSomethingElse();
}
```
* No
```csharp
if (someExpression) {
    DoSomething();
}
else
  DoSomethingElse();
```

## Single line statement
* **Don't** use single line statement blocks

### Example
* Yes
```csharp
for (int i = 0; i < 100; i++) 
{
    DoSomething(i); 
}
```
* No
```csharp
for (int i=0; i<100; i++) { DoSomething(i); }
```

## Commenting
* Use the // (two slashes) style of comment tags

### Example
* Yes
```csharp
// Create a new ray against the ground
```
* No
```csharp
/// Create a new ray against the ground
/* Create a new ray against the ground */
```

## Spacing
* Use a single space after a comma between function arguments
* **Don't** use a space after the parenthesis
* **Don't** use spaces between a function name and parenthesis
* **Don't** use spaces inside brackets
* Use a single space before flow control statements
* Use a single space before and after binary operators

### Example
* Yes
```csharp
Console.In.Read(myChar, 0, 1)
CreateFoo(myChar, 0, 1)
CreateFoo()
x = dataArray[index]
while (x == y)
int a = 17 + 52 - 23 * 3
```
* No
```csharp
Console.In.Read(myChar,0,1)
CreateFoo( myChar, 0, 1 )
CreateFoo ()
x = dataArray[ index ]
while(x==y)
int a=17 +52   -23*3
```

## Naming
* **Don't** use Hungarian notation
* **Don't** use a prefix for member variables
* Use camelCasing for member variables, parameters and local variables
* Use PascalCasing for function, property, event, and class names
* Use UPPER\_CASE for constants
* Prefix interfaces names with “I”
* **Don't** prefix enums, classes, or delegates with any letter

### Example
* Yes
```csharp
String fileName
int count
double veryLongNumber
public class MyClass
private const int WINDOW_SIZE
interface IMovable
enum Days
```
* No
```csharp
String sName
int _count
double Very_Long_number
public class my_class
private const int WindowSize
interface movableInterface
enum tag_Days
```
