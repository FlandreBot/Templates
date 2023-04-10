using Flandre.Framework;
using Flandre.Templates.Framework;
using Microsoft.Extensions.Hosting;

var builder = FlandreApp.CreateBuilder(args);

// 安装一个适配器，并添加在这里。
// builder.Adapters.Add(new YourAdapter());

builder.Plugins.Add<ExamplePlugin>();

var app = builder.Build();

// 添加内置中间件。
// 这些中间件保证 Flandre 的正常运转。你也可以加入自己的中间件，并灵活调整其顺序。
app.UseCommandSession();
// app.UseMiddleware(async (ctx, next) => { /* ... */ });
app.UseCommandParser();
app.UseCommandInvoker();

app.Run();