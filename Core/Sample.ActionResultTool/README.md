## 简介
### ActionResult

模仿ASP.Net，添加了两种对象`ActionResult`和`ActionResult<T>`,用于返回统一的任务结果

这两种对象包含`Succeeded`和`Value`两种属性，以及一些常用的隐式转换方法

### Http调用

添加了部分关于Http接口调用的扩展方法，Http调用的返回值包括和Json正反序列化的相关方法

通过这个项目中的扩展方法出入的Json会使用驼峰命名，与C#中的命名风格保持一致

## 备注

但这个项目中的代码还有一定优化空间，所以我将命名空间改成了Sample，建议大家按需选用，例如
- 对于Json序列化的默认配置，修改成了驼峰
- 对于Http调用返回值的错误，会尝试使用一种特定的Error格式去解析（这个格式是我在项目中使用的，不一定符合实际情况）
- 隐式转换逻辑在`ActionResult`和`ActionResult<T>`中略有区别
- ...