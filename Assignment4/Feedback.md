# Questions

## 1. Value objects vs primitives
What is a better practice to use when making the create function on an entity. I have gone with primitives, but I wonder is it better to use value objects?

## 2. Dependencies
The way I am handling dependencies is I have a reference to the object, example Invitation class has a reference to the guest. When I am making the create function should I pass the guest id, or the guest object?\
The reason I am asking this is we still did not implement the database (repositories) at this stage, and by passing the guest id I could not access the full Guest object at this point.