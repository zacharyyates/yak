using System;

namespace System.Numerics
{
    public static class NumericExtensions
    {
        static double OrderOfMagnitudeImpl(double number)
        {
            return Math.Floor(Math.Log10(number));
        }
        
        public static double OrderOfMagnitude(this double number)
        {
            return OrderOfMagnitudeImpl(number);
        }
        public static double OrderOfMagnitude(this long number)
        {
            return OrderOfMagnitudeImpl(number);
        }
        public static double OrderOfMagnitude(this int number)
        {
            return OrderOfMagnitudeImpl(number);
        }
        public static double OrderOfMagnitude(this short number)
        {
            return OrderOfMagnitudeImpl(number);
        }
        public static double OrderOfMagnitude(this float number)
        {
            return OrderOfMagnitudeImpl(number);
        }
        public static double OrderOfMagnitude(this decimal number)
        {
            return OrderOfMagnitudeImpl((double)number);
        }

        static int GetScale(double orderOfMagnitude)
        {
            if (orderOfMagnitude < -24) 
                throw new ArgumentOutOfRangeException("orderOfMagnitude", "Only orders of magnitude between -24 and 24 are supported.");

            if (orderOfMagnitude < -21) return -21;
            if (orderOfMagnitude < -18) return -18;
            if (orderOfMagnitude < -15) return -15;
            if (orderOfMagnitude < -12) return -12;
            if (orderOfMagnitude < -9) return -9;
            if (orderOfMagnitude < -6) return -6;
            if (orderOfMagnitude <= -3) return -3;
            if (orderOfMagnitude <= -2) return -2;
            if (orderOfMagnitude <= -1) return -1;
            if (orderOfMagnitude <= 0) return 0;
            if (orderOfMagnitude <= 1) return 1;
            if (orderOfMagnitude <= 2) return 2;
            if (orderOfMagnitude < 6) return 3;
            if (orderOfMagnitude < 9) return 6;
            if (orderOfMagnitude < 12) return 9;
            if (orderOfMagnitude < 15) return 12;
            if (orderOfMagnitude < 18) return 15;
            if (orderOfMagnitude < 21) return 18;
            if (orderOfMagnitude < 24) return 21;
            if (orderOfMagnitude < 27) return 24;
            
            // other orders of magnitude are not supported
            throw new ArgumentOutOfRangeException("orderOfMagnitude", "Only orders of magnitude between -24 and 26 are supported.");
        }

        static LongScale LongScaleImpl(double number)
        {
            return (LongScale)GetScale(OrderOfMagnitudeImpl(number));
        }

        public static LongScale LongScale(this double number)
        {
            return LongScaleImpl(number);
        }
        public static LongScale LongScale(this long number)
        {
            return LongScaleImpl(number);
        }
        public static LongScale LongScale(this int number)
        {
            return LongScaleImpl(number);
        }
        public static LongScale LongScale(this short number)
        {
            return LongScaleImpl(number);
        }
        public static LongScale LongScale(this float number)
        {
            return LongScaleImpl(number);
        }
        public static LongScale LongScale(this decimal number)
        {
            return LongScaleImpl((double)number);
        }

        static ShortScale ShortScaleImpl(double number)
        {
            return (ShortScale)GetScale(OrderOfMagnitudeImpl(number));
        }

        public static ShortScale ShortScale(this double number)
        {
            return ShortScaleImpl(number);
        }
        public static ShortScale ShortScale(this long number)
        {
            return ShortScaleImpl(number);
        }
        public static ShortScale ShortScale(this int number)
        {
            return ShortScaleImpl(number);
        }
        public static ShortScale ShortScale(this short number)
        {
            return ShortScaleImpl(number);
        }
        public static ShortScale ShortScale(this float number)
        {
            return ShortScaleImpl(number);
        }
        public static ShortScale ShortScale(this decimal number)
        {
            return ShortScaleImpl((double)number);
        }

        static Prefix PrefixImpl(double number)
        {
            return (Prefix)GetScale(OrderOfMagnitudeImpl(number));
        }

        public static Prefix Prefix(this double number)
        {
            return PrefixImpl(number);
        }
        public static Prefix Prefix(this long number)
        {
            return PrefixImpl(number);
        }
        public static Prefix Prefix(this int number)
        {
            return PrefixImpl(number);
        }
        public static Prefix Prefix(this short number)
        {
            return PrefixImpl(number);
        }
        public static Prefix Prefix(this float number)
        {
            return PrefixImpl(number);
        }
        public static Prefix Prefix(this decimal number)
        {
            return PrefixImpl((double)number);
        }

        static string GetSymbolByScale(int scale)
        {
            switch (scale)
            {
                case -24: return "y";
                case -21: return "z";
                case -18: return "a";
                case -15: return "f";
                case -12: return "p";
                case -9: return "n";
                case -6: return "µ";
                case -3: return "m";
                case -2: return "c";
                case -1: return "d";

                default:
                case 0: return string.Empty;

                case 1: return "da";
                case 2: return "h";
                case 3: return "k";
                case 6: return "M";
                case 9: return "G";
                case 12: return "T";
                case 15: return "P";
                case 18: return "E";
                case 21: return "Z";
                case 24: return "Y";
            }
        }
        static string SymbolImpl(double number)
        {
            return GetSymbolByScale(GetScale(OrderOfMagnitudeImpl(number)));
        }

        public static string Symbol(this double number)
        {
            return SymbolImpl(number);
        }
        public static string Symbol(this long number)
        {
            return SymbolImpl(number);
        }
        public static string Symbol(this int number)
        {
            return SymbolImpl(number);
        }
        public static string Symbol(this short number)
        {
            return SymbolImpl(number);
        }
        public static string Symbol(this float number)
        {
            return SymbolImpl(number);
        }
        public static string Symbol(this decimal number)
        {
            return SymbolImpl((double)number);
        }

        static string ToScaleImpl(double number)
        {
            if (number < 10000)
                return number.ToString();

            var scale = GetScale(OrderOfMagnitudeImpl(number));
            var result = number / Math.Pow(10, scale);

            var decimals = 3 - (int)OrderOfMagnitudeImpl(result);
            var rounded = Math.Round(result, decimals);
            return "{0}{1}".FormatWith(rounded, GetSymbolByScale(scale));
        }

        public static string ToScale(this double number)
        {
            return ToScaleImpl(number);
        }
        public static string ToScale(this long number)
        {
            return ToScaleImpl(number);
        }
        public static string ToScale(this int number)
        {
            return ToScaleImpl(number);
        }
        public static string ToScale(this short number)
        {
            return ToScaleImpl(number);
        }
        public static string ToScale(this float number)
        {
            return ToScaleImpl(number);
        }
        public static string ToScale(this decimal number)
        {
            return ToScaleImpl((double)number);
        }
    }

    public enum ShortScale
    {
        Septillionth = -24,
        Sextillionth = -21,
        Quintillionth = -18,
        Quadrillionth = -15,
        Trillionth = -12,
        Billionth = -9,
        Millionth = -6,
        Thousandth = -3,
        Hundredth = -2,
        Tenth = -1,
        One = 0,
        Ten = 1,
        Hundred = 2,
        Thousand = 3,
        Million = 6,
        Billion = 9,
        Trillion = 12,
        Quadrillion = 15,
        Quintillion = 18,
        Sextillion = 21,
        Septillion = 24
    }

    public enum LongScale
    {
        Quadrillionth = -24,
        Trilliardth = -21,
        Trillionth = -18,
        Billiardth = -15,
        Billionth = -12,
        Milliardth = -9,
        Millionth = -6,
        Thousandth = -3,
        Hundredth = -2,
        Tenth = -1,
        One = 0,
        Ten = 1,
        Hundred = 2,
        Thousand = 3,
        Million = 6,
        Milliard = 9,
        Billion = 12,
        Billiard = 15,
        Trillion = 18,
        Trilliard = 21,
        Quadrillion = 24
    }

    public enum Prefix
    {
        Yocto = -24,
        Zepto = -21,
        Atto = -18,
        Femto = -15,
        Pico = -12,
        Nano = -9,
        Micro = -6,
        Milli = -3,
        Centi = -2,
        Deci = -1,
        None = 0,
        Deca = 1,
        Hecto = 2,
        Kilo = 3,
        Mega = 6,
        Giga = 9,
        Tera = 12,
        Peta = 15,
        Exa = 18,
        Zetta = 21,
        Yotta = 24
    }
}