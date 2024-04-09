#pragma warning disable CS0219, IDE0044, IDE0051, IDE0059, IDE0060

// 命名空间使用帕斯卡命名
namespace testNamespace { } // IDE1006
namespace TestNamespace { }


namespace EditorconfigTest
{
    // 类、结构体、枚举、委托使用帕斯卡命名
    public class testClass { } // IDE1006
    public class TestClass { }

    public delegate void testDelegate(); // IDE1006
    public delegate void TestDelegate();

    public struct testStruct { } // IDE1006
    public struct TestStruct { }

    enum testEnum { } // IDE1006
    enum TestEnum { }

    public class TestFields
    {
        // 私有字段使用下划线驼峰命名
        private int _privateField;
        private int privateField; // IDE1006
        private int PrivateField; // IDE1006

        // 方法使用帕斯卡命名
        public void _testMethod() { } // IDE1006
        public void testMethod() { } // IDE1006
        private void TestMethodPrivate() { }
        public void TestMethodPublic() { }

        // 参数使用驼峰命名
        public void TestMethod(
            int _parameter, // IDE1006
            int parameter,
            int Parameter) // IDE1006
        {
            // 局部变量使用驼峰命名
            int _localVariable = 0; // IDE1006
            int localVariable = 0;
            int LocalVariable = 0; // IDE1006
        }

        // 常量使用大写下划线命名
        public const int _constField = 0; // IDE1006
        public const int constField = 0; // IDE1006
        public const int ConstField = 0; // IDE1006
        public const int CONST_FIELD = 0;

        // 泛型参数使用T开头的帕斯卡命名 
        public void TestMethodWithType1<T>() { }
        public void TestMethodWithType2<Tparameter>() { } // IDE1006
        public void TestMethodWithType3<TParameter>() { }
        public void TestMethodWithType4<KParameter>() { } // IDE1006
    }

    // 接口使用I开头的帕斯卡命名
    interface testInterface { } // IDE1006
    interface TestInterface { } // IDE1006
    interface ITestInterface { }
    interface ItestInterface { } // IDE1006
}
