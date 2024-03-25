## 内容
AutoCAD相关的项目

## 插件加载方式
目前没有做其他处理，dll编译生成后需要在对应版本CAD中手动使用 `NETLOAD` 命令加载dll

如果希望实现自动加载，可以考虑两个方式
- 编写自动加载的lsp文件
- 使用Autodesk的`PackageContents.xml`机制进行自动加载，需要在生成后将某些文件复制到Autodesk的指定路径下
	（具体略）

## 注意事项
每个项目需要依赖该版本AutoCAD提供的dll
目前选择了ModPlus提供的Nuget包，不过这并不是什么官方的包，如果有更好的选择也可以用另外的

每个项目的launchSettings.json需要单独配置，指向对应版本的AutoCAD