using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GalacticMerchant.Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ToDecimal()
        {
            // Arrange
            var testPatterns = new Dictionary<int, string> {
                // 1-99 Test cases from http://literacy.kent.edu/Minigrants/Cinci/romanchart.htm
                {1,"I"}, {2,"II"}, {3,"III"}, {4,"IV"}, {5,"V"}, {6,"VI"}, {7,"VII"}, {8,"VIII"}, {9,"IX"}, {10,"X"}, 
                {11,"XI"}, {12,"XII"}, {13,"XIII"}, {14,"XIV"}, {15,"XV"}, {16,"XVI"}, {17,"XVII"}, {18,"XVIII"}, {19,"XIX"}, {20,"XX"}, 
                {21,"XXI"}, {22,"XXII"}, {23,"XXIII"}, {24,"XXIV"}, {25,"XXV"}, {26,"XXVI"}, {27,"XXVII"}, {28,"XXVIII"}, {29,"XXIX"}, {30,"XXX"}, 
                {31,"XXXI"}, {32,"XXXII"}, {33,"XXXIII"}, {34,"XXXIV"}, {35,"XXXV"}, {36,"XXXVI"}, {37,"XXXVII"}, {38,"XXXVIII"}, {39,"XXXIX"}, 
                {40,"XL"}, {41,"XLI"}, {42,"XLII"}, {43,"XLIII"}, {44,"XLIV"}, {45,"XLV"}, {46,"XLVI"}, {47,"XLVII"}, {48,"XLVIII"}, {49,"XLIX"}, 
                {50,"L"}, {51,"LI"}, {52,"LII"}, {53,"LIII"}, {54,"LIV"}, {55,"LV"}, {56,"LVI"}, {57,"LVII"}, {58,"LVIII"}, {59,"LIX"}, {60,"LX"}, 
                {61,"LXI"}, {62,"LXII"}, {63,"LXIII"}, {64,"LXIV"}, {65,"LXV"}, {66,"LXVI"}, {67,"LXVII"}, {68,"LXVIII"}, {69,"LXIX"}, {70,"LXX"}, 
                {71,"LXXI"}, {72,"LXXII"}, {73,"LXXIII"}, {74,"LXXIV"}, {75,"LXXV"}, {76,"LXXVI"}, {77,"LXXVII"}, {78,"LXXVIII"}, {79,"LXXIX"}, 
                {80,"LXXX"}, {81,"LXXXI"}, {82,"LXXXII"}, {83,"LXXXIII"}, {84,"LXXXIV"}, {85,"LXXXV"}, {86,"LXXXVI"}, {87,"LXXXVII"}, {88,"LXXXVIII"}, {89,"LXXXIX"}, 
                {90,"XC"}, {91,"XCI"}, {92,"XCII"}, {93,"XCIII"}, {94,"XCIV"}, {95,"XCV"}, {96,"XCVI"}, {97,"XCVII"}, {98,"XCVIII"}, {99,"XCIX"},
                
                // Other < 3999 Test cases from http://www.diveintopython.net/unit_testing/romantest.html
                {100,"C"}, {500,"D"}, {1000,"M"}, {148,"CXLVIII"}, {294,"CCXCIV"}, {312,"CCCXII"}, {421,"CDXXI"}, {528,"DXXVIII"}, {621,"DCXXI"}, 
                {782,"DCCLXXXII"}, {870,"DCCCLXX"}, {941,"CMXLI"}, {1043,"MXLIII"}, {1110,"MCX"}, {1226,"MCCXXVI"}, {1301,"MCCCI"}, {1485,"MCDLXXXV"}, 
                {1509,"MDIX"}, {1607,"MDCVII"}, {1754,"MDCCLIV"}, {1832,"MDCCCXXXII"}, {1993,"MCMXCIII"}, {2074,"MMLXXIV"}, {2152,"MMCLII"}, {2212,"MMCCXII"}, 
                {2343,"MMCCCXLIII"}, {2499,"MMCDXCIX"}, {2574,"MMDLXXIV"}, {2646,"MMDCXLVI"}, {2723,"MMDCCXXIII"}, {2892,"MMDCCCXCII"}, {2975,"MMCMLXXV"}, 
                {3051,"MMMLI"}, {3185,"MMMCLXXXV"}, {3250,"MMMCCL"}, {3313,"MMMCCCXIII"}, {3408,"MMMCDVIII"}, {3501,"MMMDI"}, {3610,"MMMDCX"}, 
                {3743,"MMMDCCXLIII"}, {3844,"MMMDCCCXLIV"}, {3888,"MMMDCCCLXXXVIII"}, {3940,"MMMCMXL"}, {3999,"MMMCMXCIX"},
            };

            // Act // Assert
            foreach (var pair in testPatterns) {
                Assert.AreEqual(pair.Key, RomanNumber.ToDecimal(pair.Value));
            }
        }

        [TestMethod]
        public void ValidateSubtractSequenceAlwaysOneLong()
        {
            // Arrange
            int arabNumber;
            // Act // Assert
            Assert.IsFalse(RomanNumber.TryToDecimal("IIIX", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("IIIX", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("IIIX", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("IIIX", out arabNumber));
        }

        [TestMethod]
        public void ValidateCantAggregateSubtracts()
        {
            // Arrange
            int arabNumber;
            // Act // Assert
            Assert.IsFalse(RomanNumber.TryToDecimal("IXC", out arabNumber));
        }

        [TestMethod]
        public void ValidateInvalidDigits()
        {
            // Arrange
            int arabNumber;
            // Act // Assert
            Assert.IsFalse(RomanNumber.TryToDecimal("A", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("XA", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("XA", out arabNumber));
        }
        
        [TestMethod]
        public void ValidateNotAllowedToSubtract()
        {
            // Arrange
            int arabNumber;
            // Act // Assert
            Assert.IsFalse(RomanNumber.TryToDecimal("IL", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("IC", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("DM", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("VX", out arabNumber));
        }
        
        [TestMethod]
        public void ValidateSequenceTooLong()
        {
            // Arrange
            int arabNumber;
            // Act // Assert
            Assert.IsFalse(RomanNumber.TryToDecimal("VV", out arabNumber));
            Assert.IsFalse(RomanNumber.TryToDecimal("IIII", out arabNumber));
        }

    }
}
