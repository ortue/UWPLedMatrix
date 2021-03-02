﻿//using System;

//namespace WebMatrix.Classes
//{
//  public struct Complex
//  {
//    public double Real;
//    public double Imaginary;
//    public double MagnitudeSquared { get { return Real * Real + Imaginary * Imaginary; } }
//    public double Magnitude { get { return Math.Sqrt(MagnitudeSquared); } }

//    public Complex(double real, double imaginary)
//    {
//      Real = real;
//      Imaginary = imaginary;
//    }

//    public override string ToString()
//    {
//      if (Imaginary < 0)
//        return $"{Real}-{-Imaginary}j";
//      else
//        return $"{Real}+{Imaginary}j";
//    }

//    public static Complex operator +(Complex a, Complex b)
//    {
//      return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
//    }

//    public static Complex operator -(Complex a, Complex b)
//    {
//      return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
//    }

//    public static Complex operator *(Complex a, Complex b)
//    {
//      return new Complex(
//          real: (a.Real * b.Real) - (a.Imaginary * b.Imaginary),
//          imaginary: (a.Real * b.Imaginary) + (a.Imaginary * b.Real));
//    }

//    public static Complex operator *(Complex a, double b)
//    {
//      return new Complex(a.Real * b, a.Imaginary * b);
//    }

//    public static Complex[] FromReal(double[] real)
//    {
//      Complex[] complex = new Complex[real.Length];
//      for (int i = 0; i < real.Length; i++)
//        complex[i].Real = real[i];
//      return complex;
//    }

//    public static double[] GetMagnitudes(Complex[] input)
//    {
//      double[] output = new double[input.Length];
//      for (int i = 0; i < input.Length; i++)
//        output[i] = input[i].Magnitude;
//      return output;
//    }
//  }
//}
