using Flandre.Core.Messaging;
using Flandre.Framework.Attributes;
using Flandre.Framework.Common;

namespace Flandre.Templates.Framework;

public sealed class ExamplePlugin : Plugin
{
    /// <summary>
    /// 每次收到消息时触发。
    /// </summary>
    public override async Task OnMessageReceived(MessageContext ctx)
    {
        // 如果消息文本为 "Hello!"，
        if (ctx.Message.GetText() == "Hello!")
        {
            // 则发送一条 "World!" 消息作为回复。
            await ctx.Bot.SendMessage(ctx.Message, "World!");
        }
    }

    /// <summary>
    /// 定义一条指令。<br/>
    /// 向机器人发送 "example (num)" 时触发，其中 num 为一个合法的 double 值。
    /// </summary>
    /// 
    /// <param name="num">
    /// 定义一个参数。框架会自动识别参数的类型，并解析用户传入的文本。
    /// </param>
    /// 
    /// <returns>
    /// 指令方法需要返回机器人的回复消息。
    /// </returns>
    ///
    /// <example>
    /// 发送：example 1.28 <br/>
    /// 回复：1.28 的二次方为 1.6384。
    /// </example>
    [Command("example")]
    public string OnExampleCommand(double num)
    {
        return $"{num} 的二次方为 {Math.Pow(num, 2)}。";
    }
}