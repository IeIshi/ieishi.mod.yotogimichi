using BepInEx.Logging;
using System;
using System.CodeDom;
using System.Linq.Expressions;

namespace ieishi.mod.yotogimichi
{
    public static class Logging
    {
        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            var log = new ManualLogSource(PluginInfo.PLUGIN_NAME); // The source name is shown in BepInEx log
            Logger.Sources.Add(log);
            log.Log(level, data);
            Logger.Sources.Remove(log);
        }

        /// <summary>
        /// usage: Logging.LogVariable(() => Variable);
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="level"></param>
        public static void LogVariable(Expression<Func<object>> variable, LogLevel level = LogLevel.Info)
        {
            var log = new ManualLogSource(PluginInfo.PLUGIN_NAME); // The source name is shown in BepInEx log
            Logger.Sources.Add(log);

            var varname = variable.Body is MemberExpression ? ((MemberExpression)variable.Body).Member.Name : (((UnaryExpression)variable.Body).Operand as MemberExpression)?.Member.Name;
            var vartype = variable.Body is MemberExpression ? ((MemberExpression)variable.Body).Type : (((UnaryExpression)variable.Body).Operand as MemberExpression)?.Type;
            var varvalue = variable.Compile().Invoke();

            var isarray = (varvalue is Array);

            if (isarray)
            {
                var i = 0;
                foreach (var item in varvalue as Array)
                {
                    log.Log(level, varname + "[" + i + "]   (" + item.GetType() + ")    " + item);
                    i++;
                }
            }
            else
            {
                log.Log(level, varname + "  (" + vartype + ")   " + varvalue);
            }

            Logger.Sources.Remove(log);
        }
    }
}
