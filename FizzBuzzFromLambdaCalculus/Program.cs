using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzzFromLambdaCalculus
{
    delegate dynamic F0();
    delegate dynamic F1(dynamic a);
    delegate F1 F2(dynamic a);
    delegate F2 F3(dynamic a);
    delegate F3 F4(dynamic a);
    delegate F4 F5(dynamic a);
    delegate F5 F6(dynamic a);
    
    class Program
    {
        static F1 Y = f => ((F1)(x => f(x(x))))((F1)(x => f(x(x))));
        static F1 Z = f => ((F1)(x => f((F1)(y => x(x)(y)))))((F1)(x => f((F1)(y => x(x)(y)))));

        static F2 ZERO = proc => x => x;
        static F2 ONE = proc => x => proc(x);
        static F2 TWO = proc => x => proc(proc(x));
        static F2 THREE = proc => x => proc(proc(proc(x)));
        static F2 FOUR = proc => x => proc(proc(proc(proc(x))));
        static F2 FIVE = proc => x => proc(proc(proc(proc(proc(x)))));

        static F2 TRUE = x => y => x;
        static F2 FALSE = x => y => y;

        static F3 IF = proc => x => y => proc(x)(y);

        static F1 IS_ZERO = proc => proc((F1)(x => FALSE))(TRUE);
        static F2 IS_LESS_OR_EQUAL = m => n => IS_ZERO(SUB(m)(n));

        static F3 INC = n => p => x => p(n(p)(x));
        static F3 DEC = n => f => x => n((F2)(g => h => h(g(f))))((F1)(y => x))((F1)(y => y));
        
        static F2 SIX = INC(FIVE);
        static F2 SEVEN = INC(SIX);
        static F2 EIGHT = INC(SEVEN);
        static F2 NINE = INC(EIGHT);
        static F2 TEN = INC(NINE);

        static F2 ADD = m => n => n(INC)(m);
        static F2 SUB = m => n => n(DEC)(m);
        static F2 MUL = m => n => n(ADD(m))(ZERO);
        static F2 POW = m => n => n(MUL(m))(ONE);
        static F2 DIV = Z((F3)(f => m => n => IF(IS_LESS_OR_EQUAL(n)(m))((F1)(x => INC(f(SUB(m)(n))(n))(x)))(ZERO)));
        static F2 MOD = Z((F3)(f => m => n => IF(IS_LESS_OR_EQUAL(n)(m))((F1)(x => f(SUB(m)(n))(n)(x)))(m)));

        static F3 PAIR = x => y => f => f(x)(y);
        static F1 LEFT = p => p((F2)(x => y => x));
        static F1 RIGHT = p => p((F2)(x => y => y));

        static F1 EMPTY = PAIR(TRUE)(TRUE);
        static F3 UNSHIFT = l => x => PAIR(FALSE)(PAIR(x)(l));
        static F1 IS_EMPTY = LEFT;
        static F1 FIRST = l => LEFT(RIGHT(l));
        static F1 REST = l => RIGHT(RIGHT(l));

        static F2 RANGE = Z((F3)(f => m => n => IF(IS_LESS_OR_EQUAL(m)(n))((F1)(x => UNSHIFT(f(INC(m))(n))(m)(x)))(EMPTY)));

        static F3 FOLD = Z((F4)(f => l => x => g => IF(IS_EMPTY(l))(x)((F1)(y => g(f(REST(l))(x)(g))(FIRST(l))(y)))));
        static F2 MAP = k => f => FOLD(k)(EMPTY)((F2)(l => x => UNSHIFT(l)(f(x))));
        static F2 PUSH = l => x => FOLD(l)(UNSHIFT(EMPTY)(x))(UNSHIFT);

        static F1 TO_DIGITS = Z((F2)(f => n => PUSH(IF(IS_LESS_OR_EQUAL(n)(NINE))(EMPTY)((F1)(x => f(DIV(n)(TEN))(x))))(MOD(n)(TEN))));
        
        static F1 NUM2CHAR = ADD(MUL(SIX)(EIGHT));
        
        static F2 CHAR_F = MUL(SEVEN)(TEN);
        static F2 CHAR_I = ADD(MUL(TEN)(TEN))(FIVE);
        static F2 CHAR_Z = ADD(POW(INC(TEN))(TWO))(ONE);
        static F2 CHAR_B = MUL(SIX)(INC(TEN));
        static F2 CHAR_U = ADD(MUL(INC(TEN))(TEN))(SEVEN);

        static F2 FIFTEEN = MUL(FIVE)(THREE);
        static F2 HUNDRED = POW(TEN)(TWO);
        
        static F1 FIZZ = UNSHIFT(UNSHIFT(UNSHIFT(UNSHIFT(EMPTY)(CHAR_Z))(CHAR_Z))(CHAR_I))(CHAR_F);
        static F1 BUZZ = UNSHIFT(UNSHIFT(UNSHIFT(UNSHIFT(EMPTY)(CHAR_Z))(CHAR_Z))(CHAR_U))(CHAR_B);
        static F1 FIZZBUZZ = UNSHIFT(UNSHIFT(UNSHIFT(UNSHIFT(BUZZ)(CHAR_Z))(CHAR_Z))(CHAR_I))(CHAR_F);

        static void Main(string[] args)
        {
            DoFizzBuzz();
        }

        static void DoFizzBuzz()
        {
            var f = MAP(RANGE(ONE)(HUNDRED))((F1)(n =>
                IF(IS_ZERO(MOD(n)(FIFTEEN)))(
                    FIZZBUZZ
                )(IF(IS_ZERO(MOD(n)(THREE)))(
                    FIZZ
                )(IF(IS_ZERO(MOD(n)(FIVE)))(
                    BUZZ
                )(
                    MAP(TO_DIGITS(n))(NUM2CHAR)
            )))));
            
            List<dynamic> string_list = to_list(f);
            string_list
                .ToList()
                .ForEach(s => print_string(s));
        }

        static void TestInteger()
        {
            Console.WriteLine(to_integer(ZERO));
            Console.WriteLine(to_integer(ONE));
            Console.WriteLine(to_integer(TWO));
            Console.WriteLine(to_integer(THREE));
            
            Console.WriteLine(to_boolean(IS_ZERO(ONE)));
            Console.WriteLine(to_boolean(IS_ZERO(ZERO)));
        }

        static void TestBoolean()
        {
            Console.WriteLine(to_boolean(TRUE));
            Console.WriteLine(to_boolean(FALSE));
        }

        static void TestNumericOperation()
        {
            Console.WriteLine(to_integer(ADD(TWO)(THREE)));
            Console.WriteLine(to_integer(SUB(THREE)(TWO)));
            Console.WriteLine(to_integer(MUL(TWO)(THREE)));

            Console.WriteLine(to_integer(MOD(TEN)(SIX)));
            Console.WriteLine(to_integer(POW(TEN)(TWO)));
            
            Console.WriteLine(to_boolean(IS_LESS_OR_EQUAL(FOUR)(THREE)));
            Console.WriteLine(to_boolean(IS_LESS_OR_EQUAL(THREE)(THREE)));
            Console.WriteLine(to_boolean(IS_LESS_OR_EQUAL(TWO)(THREE)));
        }

        static void TestPair()
        {
            var pair = PAIR(ONE)(TWO);
            
            Console.WriteLine(to_integer(LEFT(pair)));
            Console.WriteLine(to_integer(RIGHT(pair)));
            Console.WriteLine(to_integer(ONE));
            Console.WriteLine(to_integer(TWO));
        }

        static void TestList()
        {
            var list = UNSHIFT(
                UNSHIFT(
                    UNSHIFT(EMPTY)(THREE)
                )(TWO)
            )(ONE);

            var list1 = build_list(ONE, TWO, THREE);
            
            Console.WriteLine(to_integer(FIRST(list)));
            Console.WriteLine(to_integer(FIRST(list1)));
            
            Console.WriteLine(to_integer(FIRST(REST(list))));
            Console.WriteLine(to_integer(FIRST(REST(list1))));
            
            Console.WriteLine(to_integer(FIRST(REST(REST(list)))));
            Console.WriteLine(to_integer(FIRST(REST(REST(list1)))));
            
            Console.WriteLine(to_boolean(IS_EMPTY(list)));
            Console.WriteLine(to_boolean(IS_EMPTY(list1)));
            
            Console.WriteLine(to_boolean(IS_EMPTY(EMPTY)));

            print_int_list(list1);
        }

        static void TestRange()
        {
            var range = RANGE(ONE)(POW(TEN)(TWO));
            print_int_list(range);
        }

        static void TestFold()
        {
            F1 pow2 = x => POW(x)(TWO); 
            var list = MAP(RANGE(ONE)(FIVE))(pow2);
            print_int_list(list);
        }

        static void TestDigits()
        {
            var _125 = POW(FIVE)(THREE);
            
            print_int_list(TO_DIGITS(_125));
        }

        static int to_integer(dynamic proc)
            => proc((F1)(n => n + 1))(0);

        static bool to_boolean(dynamic proc)
            => IF(proc)(true)(false); // same as proc(true)(false)

        static char to_char(dynamic proc)
            => (char)to_integer(proc);

        static dynamic build_list(params dynamic[] elems)
        {
            return elems.Length == 0
                ? EMPTY
                : UNSHIFT(build_list(elems.Skip(1).ToArray()))(elems.First());
        }

        static List<dynamic> to_list(dynamic proc)
        {
            var list = new List<dynamic>();

            while (!to_boolean(IS_EMPTY(proc)))
            {
                list.Add(FIRST(proc));
                proc = REST(proc);
            }

            return list;
        }

        static void print_int_list(dynamic list)
        {
            List<dynamic> unpacked = to_list(list);
            Console.WriteLine(string.Join(", ", unpacked.Select(to_integer)));
        }
        
        static void print_string(dynamic list)
        {
            List<dynamic> unpacked = to_list(list);
            Console.WriteLine(string.Join("", unpacked.Select(to_char)));
        }
    }
}