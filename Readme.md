# λ-FizzBuzz in C#

Inspired by [programming with nothing](https://codon.com/programming-with-nothing) by [Tom Stuart](https://github.com/tomstuart).
C# code here is just ported from his ruby code.

No number literals, no booleans, no strings or list, only with **PURE** Lambda.

Full code: [here](/FizzBuzzFromLambdaCalculus/Program.cs)

## FIZZBUZZ LAMBDA

```csharp
MAP(RANGE(ONE)(HUNDRED))((F1)(n =>
    IF(IS_ZERO(MOD(n)(FIFTEEN)))(
        FIZZBUZZ
    )(IF(IS_ZERO(MOD(n)(THREE)))(
        FIZZ
    )(IF(IS_ZERO(MOD(n)(FIVE)))(
        BUZZ
    )(
        MAP(TO_DIGITS(n))(NUM2CHAR)
)))));
```

And it works, perfectly!!

```
1
2
Fizz
4
Buzz
Fizz
7
...
```
