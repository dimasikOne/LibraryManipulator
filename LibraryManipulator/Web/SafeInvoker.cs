using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LibraryManipulator.Web
{
    public class SafeInvoker
    {
        private static readonly ILogger Logger = Log.ForContext<SafeInvoker>();
        public static async Task<IActionResult> InvokeAsync<TArg, TResult>(Func<TArg, Task<TResult>> func, TArg argument)
        {
            try
            {
                var result = await func.Invoke(argument);
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Exception while trying to invoke delegate function");
                return new BadRequestResult();
            }
        }
        
        public static async Task<IActionResult> InvokeAsync<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return new ObjectResult(await func.Invoke());
            }
            catch (Exception e)
            {
                Logger.Error(e, "Exception while trying to invoke delegate function");
                return new BadRequestResult();
            }
        }
        
        public static async Task<IActionResult> InvokeAsync<TArg>(Func<TArg, Task> func, TArg arg)
        {
            try
            {
                await func.Invoke(arg);
                return new AcceptedResult();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Exception while trying to invoke delegate function");
                return new BadRequestResult();
            }
        }

        public static IActionResult Invoke<TArg>(Action<TArg> func, TArg argument)
        {
            try
            {
                func.Invoke(argument);
                return new AcceptedResult();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Exception while trying to invoke delegate function");
                return new BadRequestResult();
            }
        }
    }
}