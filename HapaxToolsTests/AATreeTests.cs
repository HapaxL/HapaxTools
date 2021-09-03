using System;
using Xunit;
using Xunit.Abstractions;
using HapaxTools;

namespace HapaxToolsTests
{
    public class AATreeTests
    {
        public ITestOutputHelper stdout { get; private set; }

        public AATreeTests(ITestOutputHelper output)
        {
            stdout = output;
        }

        /*
        public class InsertBasicTests
        {
            [Fact]
            public void InsertBasic()
            {
                var tree = new AATree<int>();
                Assert.Equal("AATree []", tree.ToString());
                tree.Insert(2);
                Assert.Equal("AATree [( 2 )]", tree.ToString());
                tree.Insert(1);
                Assert.Equal("AATree [( 1 ( 2 ))]", tree.ToString());
                tree.Insert(4);
                Assert.Equal("AATree [(( 1 ) 2 ( 4 ))]", tree.ToString());
                tree.Insert(3);
                Assert.Equal("AATree [(( 1 ) 2 ( 3 ( 4 )))]", tree.ToString());
            }
        }

        public class InsertLeftmostTests
        {
            [Fact]
            public void InsertBasic()
            {
                var tree = new AATree<int>();
                Assert.Equal("AATree []", tree.ToString());
                tree.Insert(2);
                Assert.Equal("AATree [( 2 )]", tree.ToString());
                tree.Insert(1);
                Assert.Equal("AATree [( 1 ( 2 ))]", tree.ToString());
                tree.Insert(2);
                Assert.Equal("AATree [(( 1 ) 2 ( 2 ))]", tree.ToString());
                tree.Insert(3);
                Assert.Equal("AATree [(( 1 ) 2 ( 2 ( 3 )))]", tree.ToString());
            }
        }
        */
        /* DeleteAny tests */
        [Fact]
        public void DeleteAnyBasic()
        {
            var tree = new AATree<int>();
            int n = 65;
            for (int i = n; i > 0; i--)
            {
                var v = i % 2;
                tree.Insert(v, i);
                stdout.WriteLine(tree.ToString());
            }
            for (int i = 0; i < n; i++)
            {
                var v = 2*i / n;
                tree.Delete(v);
                stdout.WriteLine(tree.ToString());
            }
        }
        /*
        [Fact]
        public void DeleteFromEmpty()
        {
            var tree = new AATree<int>();
            tree.Insert(2);
            Assert.Equal("AATree [( 2 )]", tree.ToString());
            tree.Delete(2);
            Assert.Equal("AATree []", tree.ToString());
            tree.Delete(3);
            Assert.Equal("AATree []", tree.ToString());
        }

        [Fact]
        public void DeleteNonexistent()
        {
            var tree = new AATree<int>();
            tree.Insert(2);
            Assert.Equal("AATree [( 2 )]", tree.ToString());
            tree.Delete(3);
            Assert.Equal("AATree [( 2 )]", tree.ToString());
        }
        */

        /*
        public class DeleteAllTests
        {
            [Fact]
            public void DeleteBasic()
            {
                var tree = new AATree<int>(defaultDeleteBehaviour: AATree<int>.DuplicateDeleteBehaviour.DeleteAll);
                tree.Insert(2);
                tree.Insert(1);
                tree.Insert(2);
                tree.Insert(3);
                Assert.Equal("AATree [(( 1 ) 2 ( 2 ( 3 )))]", tree.ToString());
                tree.Delete(2);
                Assert.Equal("AATree [( 1 ( 3 ))]", tree.ToString());
                tree.Delete(1);
                Assert.Equal("AATree [( 3 )]", tree.ToString());
                tree.Delete(3);
                Assert.Equal("AATree []", tree.ToString());
            }

            [Fact]
            public void DeleteFromEmpty()
            {
                var tree = new AATree<int>(defaultDeleteBehaviour: AATree<int>.DuplicateDeleteBehaviour.DeleteAll);
                tree.Insert(2);
                Assert.Equal("AATree [( 2 )]", tree.ToString());
                tree.Delete(2);
                Assert.Equal("AATree []", tree.ToString());
                tree.Delete(3);
                Assert.Equal("AATree []", tree.ToString());
            }

            [Fact]
            public void DeleteNonexistent()
            {
                var tree = new AATree<int>(defaultDeleteBehaviour: AATree<int>.DuplicateDeleteBehaviour.DeleteAll);
                tree.Insert(2);
                Assert.Equal("AATree [( 2 )]", tree.ToString());
                tree.Delete(3);
                Assert.Equal("AATree [( 2 )]", tree.ToString());
            }
        }
        */
    }
}
