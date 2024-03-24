## 简介
这个项目主要存放View层的相关逻辑，主要是WPF在UI层的一些内容

## 二开容易遇到的一些问题
### DLL加载的问题
有些软件（例如Revit）通过某些方式加载DLL的时候，只会加载DLL本身，而不会加载DLL依赖的其他DLL。这样会导致软件无法正常运行。
需要通过修改事件`AppDomain.CurrentDomain.AssemblyResolve`来手动加载依赖的DLL。

### 多版本DLL加载冲突的问题
如果有多个插件，使用了同名而不同版本的dll，可能会出现冲突（软件可能错误地加载其他版本的dll）

### 资源加载的问题
见 HomePage.xaml文件中的注释
