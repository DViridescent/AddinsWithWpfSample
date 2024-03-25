## 内容
同AutoCAD

## 插件加载方式
在Rhino中Option->Plug-ins->Install...选择`Sample.Rhino.rhp`文件
后续Rhino会自动加载对应位置的rhp文件，不需要做额外处理

## 注意事项
不过目前Rhino只做了7版本，Rhino的版本之间的差异比较大，所以虽然使用了共享项目可能实际上也没什么必要

Rhino的嵌入页面的使用方式跟另外两个软件略有不同，其中View对象的创建由Rhino负责，
所以在代码里用`MainView`给`MainControl`又包了一层