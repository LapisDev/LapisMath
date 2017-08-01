# Lapis.Math
A collection of portable class libraries for solving math problems written in C#. Now it has been ported to .NET Core.

## Lapis.Math.Number
[Lapis.Math.Numbers](src/Lapis.Math.Numbers) mainly contains:
- Fraction;
- Complex;
- Big integer and big decimal.

## Lapis.Math.Algebra
[Lapis.Math.Algebra](src/Lapis.Math.Algebra) provides basic infrastructure for simple symbolic algebra.
- Expressions for polynomial, exponential and trigonometric functions;
- Symbolic derivative.
Code is heavily borrowed from [Math.NET Symbolics](https://github.com/mathnet/mathnet-symbolics).

## Lapis.Math.LineAlgebra
[Lapis.Math.LineAlgebra](src/Lapis.Math.LineAlgebra) provides matrices and determinant calculation.
- Addition, subtraction and multiplication of matrices;
- Inverse of a matrix;
- Minor and cofactor;
- Rank and trace;
- Determinant.
- Vectors.

## Lapis.Math.Numerical
[Lapis.Math.Numerical](src/Lapis.Math.Numerical) mainly contains:
- Numerical integration;
- Simple equation solver.

## Lapis.Math.Statistical
[Lapis.Math.Statistical](src/Lapis.Math.Statistical) mainly contains:
- Statistics calculator;
- Fitting.

## Lapis.Math.Measurement
[Lapis.Math.Measurement](src/Lapis.Math.Measurement) provides a scientific calculator with units of measure:
- Units of measure based on SI;
- Physical constants.