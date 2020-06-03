### Net Core 3.1

### 1. config log4Net
---
## 2020.6.2 core 基础
#### run web by cmd
- Center.Web\bin\Debug\netcoreapp3.1> dotnet Center.Web.dll --urls="http://*:5177" --ip="127.0.0.1"  --port=5177
#### 无法加载wwwroot文件，解决办法
1. copy wwwroot 文件夹\bin\Debug\netcoreapp3.1>
2. 在startup中设置File Path
```
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
});
   ```
