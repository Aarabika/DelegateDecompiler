﻿// Contributed by @JonPSmith (GitHub) www.thereformedprogrammer.com

using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegateDecompiler.EfTests.EfItems
{
    public class EfParent
    {

        public int EfParentId { get; set; }

        public bool ParentBool { get; set; }

        public int ParentInt { get; set; }

        public double ParentDouble { get; set; }

        public string ParentString { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan ParentTimeSpan { get; set; }

        public ICollection<EfChild> Children { get; set; }

        //----------------------------------------------------
        //Computed properties

        //LOGICAL GROUP

        [Computed]
        public bool BoolEqualsConstant { get { return ParentBool == true; } }

        private static bool staticBool = true;

        [Computed]
        public bool BoolEqualsStaticVariable { get { return ParentBool == staticBool; } }


        //EQUALITY GROUP

        [Computed]
        public bool IntEqualsConstant { get { return ParentInt == 123; } }

        private static int staticInt = 123;
        [Computed]
        public bool IntEqualsStaticVariable { get { return ParentInt == staticInt; } }

        [Computed]
        public bool IntEqualsStringLength { get { return ParentInt == ParentString.Length; } }

        [Computed]
        public bool IntNotEqualsStringLength { get { return ParentInt != ParentString.Length; } }  


        //QUANTIFIER OPERATORS

        [Computed]
        public bool AnyChildren { get { return Children.Any(); } }

        [Computed]        
        public bool AnyChildrenWithFilter { get { return Children.Any(y => y.ChildInt == 123); } }

        [Computed]
        public bool AnyCharInString { get { return ParentString.Any(y => y == '2'); } }

        [Computed]
        public bool AllFilterOnChildrenInt { get { return Children.All(y => y.ChildInt == 123); } }

        [Computed]
        public bool StringContainsConstantString { get { return ParentString.Contains("2"); } }

        //AGGREGATE GROUP

        [Computed]
        public int CountChildren { get { return Children.Count(); } }  

        [Computed]
        public int CountChildrenWithFilter { get { return Children.Count(y => y.ChildInt == 123); } }

        [Computed]
        public int SumIntInChildrenWhereChildrenCanBeNone { get { return Children.Sum(y => (int?)y.ChildInt) ?? 0; } } 


        //TYPES GROUP
        private static readonly DateTime dateConst = new DateTime(2000, 1, 1);

        [Computed]
        public bool StartDateGreaterThanStaticVar { get { return StartDate > dateConst; } } 


    }
}
