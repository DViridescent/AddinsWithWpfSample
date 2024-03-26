## 内容
同AutoCAD

## 插件加载方式
Revit会自动识别某个指定路径下的 `.addin` 文件
目前已在`Sample.Revit.2021`项目下配置了生成后事件，会自动将 `.addin` 文件和生成结果复制到指定路径下
详见[Sample.Revit.2021项目文件](Sample.Revit.2021/Sample.Revit.2021.csproj)

所以配置好Revit路径，直接运行`Sample.Revit.2021`项目即可

## 注意事项
最近找了个新的包含RevitAPI的包（Revit_All_Main_Versions_API_x64），但虽然这个包的描述里说

> Sets the references 'Copy Local' to False.

但实测发现，这个包的DLL还是会被复制到输出目录，所以还是需要手动删除相关DLL（RevitAPI.dll, RevitAPIUI.dll 等等）。

另，`SampleRevit.addin` 文件的属性已被修改，设置为会复制到输出目录，以便生成后复制到指定路径下。