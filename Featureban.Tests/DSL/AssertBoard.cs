using Featureban.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Featureban.Tests.DSL
{
    public static class AssertBoard
    {
        public static void Equals(string expectedBoard, Board actualBoard )
        {
            Assert.Equal(BoardParser.Parse(expectedBoard), actualBoard);
        }


        

    }
}
