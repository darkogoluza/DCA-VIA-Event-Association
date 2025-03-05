# Questions

## 1. Value objects vs primitives
What is a better practice to use when making the create function on an entity. I have gone with primitives, but I wonder is it better to use value objects?

## 2. Dependencies
The way I am handling dependencies is I have a reference to the object, example Invitation class has a reference to the guest. When I am making the create function should I pass the guest id, or the guest object?\
The reason I am asking this is we still did not implement the database (repositories) at this stage, and by passing the guest id I could not access the full Guest object at this point.

## 3. Testing
When testing methods such as `UpdateTitle()` and I want to test if the title is too long for example\
do I only test the `Title` class since there is where I hold my validation logic?\
Example:
```C#
 [Fact]
    public void UpdateEventTitle_TitleIsToLong()
    {
        // Arrange
        var toLongString = new string('A', 76);

        // Act
        var newTitleResult = Title.Create(toLongString);

        // Assert
        Assert.True(newTitleResult.isFailure);
        Assert.Contains(Error.BadTitle(), newTitleResult.errors);
    }
```
# Use Cases
* What about use cases that depend on other use case? how to handle that when testing?
* Do I make more of my own tests to follow ZOMBIE method?
* Do I additionally test value objects?
