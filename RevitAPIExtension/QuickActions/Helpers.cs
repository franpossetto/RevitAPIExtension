using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension.QuickActions
{
    static class Helpers
    {
        public static bool Exist(this ProjectItems items, Func<ProjectItem, bool> action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            bool result = false;
            foreach (ProjectItem item in items)
            {
                if (result)
                    break;
                if (item.ProjectItems.Count > 0)
                    result = item.ProjectItems.Exist(action);
                else
                    result = action(item);
            }
            return result;
        }
        public static void ForEach(this ProjectItems items, Action<ProjectItem> action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in items)
            {
                if (item.ProjectItems.Count > 0)
                    item.ProjectItems.ForEach(action);
                else
                    action(item);
            }
        }
        public static ProjectItem Find(this ProjectItems items, Func<ProjectItem, bool> action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            foreach (ProjectItem item in items)
            {
                if (item.ProjectItems.Count > 0)
                {
                    var result = item.ProjectItems.Find(action);
                    if (result != null)
                        return result;
                }
                else if (action(item))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
