This is a simple C# port of the code presented in this post:

https://stackoverflow.com/questions/38923376/return-a-new-string-that-sorts-between-two-given-strings

When a user needs to be able to arbitrarly re-order a list of items, a common solution is to add a hidden integer value used only for sorting.
The drawback with this approach, however, is that it when items are moved around, you often need to update multiple items to keep the ordering values rational.
This library may be used to implement a alternative approach. Instead of a numerical sort key, you can use a string.
When you insert a new item, or move an existing item to a new position in the list, you can use this library to generate a new value that lies between the values that lie before and after.
Only the item being moved needs to be modified.

Example
``` c#
var interpolator = Interpolator.SingleCaseAlpha;

var between = interpolator.GetStringBetween("abc", "abcb")

Assert.That(between, Is.EqualTo("abcam")) // when sorting, this value comes after "abc" but before "abcb"
```